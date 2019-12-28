using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using System.Xml;
using System.Xml.XPath;

namespace Indy500
{
    class Level
    {
        public string Title { get; set; }
        public Point LevelSize { get; private set; }
        public int[,] Tiles { get; private set; }
        public GameModes SupportedModes { get; set; }
        public Line StartLine { get; set; }
        public int MaxPlayers { get; set; }
        public List<Line> AIWaypoints { get; private set; }

        // TODO: Locations (and rotations) denoting spawn points for the cars in various game modes.
        // NOTE: If we ever have more than 9 different tile types, the map format will have to change slightly.
        //  Currently, LoadMap runs character-by-character, in the even we have multiple-digit tile-types, we'll
        //  need to delimit each tile via a space or something and reflect that change in LoadMap's implementation.


        public Level()
        {
            Title = "";
            SetSize(0, 0);
            SupportedModes = GameModes.None;
            StartLine = new Line(0, 0, 1, 0);
            AIWaypoints = new List<Line>();
        }

        public void SetSize(int width, int height)
        {
            LevelSize = new Point(width, height);
            Tiles = new int[width, height];
        }

        public static Level Parse(string data)
        {
            Level result = new Level();

            string[] lines = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string sectionName = "";
            string sectionData = "";
            bool isInSection = false;
            for (int i = 0; i < lines.Length; i++)
            {
                var curLine = lines[i].Trim();
                if(curLine.StartsWith("[") && curLine.EndsWith("]"))
                {
                    if(isInSection)
                    {
                        HandleSection(result, sectionName, sectionData);
                        sectionData = "";
                    }
                    sectionName = curLine.Substring(1, curLine.Length - 2);

                    if(sectionData != "")
                    {
                        Console.WriteLine($"Discarding unspecified level section containing {sectionData.Length} characters of data.");
                    }

                    sectionData = "";
                    isInSection = true;
                }
                else if(!curLine.StartsWith(";") && isInSection)
                {
                    sectionData += " " + curLine;
                }
            }

            if(isInSection)
            {
                HandleSection(result, sectionName, sectionData);
            }

            return result;
        }

        private static void HandleSection(Level level, string sectionName, string sectionData)
        {
            sectionName = sectionName.ToLower();
            if(sectionName == "metadata")
            {
                LoadMetadata(level, sectionData);
            }
            else if(sectionName == "map")
            {
                LoadMap(level, sectionData);
            }
        }

        private static void LoadMetadata(Level level, string source)
        {
            var document = new XmlDocument();
            document.LoadXml(source);

            var rootElement = document.DocumentElement;

            // Load the XML
            LoadMetadataXml(rootElement, out var data);
            // Validate the data as far as we can while they're in their string form.
            CheckNullMetadata(data);

            // Set various level attributes from the data, completing validation as we can.
            level.Title = data.Title;
            var size = LoadPoint(data.Size);
            level.SetSize(size.X, size.Y);

            if(!int.TryParse(data.Modes, out int modes))
            {
                throw new LevelParseException($"Available game modes value must be a number.{Environment.NewLine}(Data: {data.Modes})");
            }
            level.SupportedModes = (GameModes)modes;

            if (!int.TryParse(data.MaxPlayers, out int maxPlayers))
            {
                throw new LevelParseException($"Maximum players value must be a number.{Environment.NewLine}(Data: {data})");
            }
            level.MaxPlayers = maxPlayers;

            var startLineA = LoadPoint(data.StartLineStart);
            var startLineB = LoadPoint(data.StartLineEnd);
            level.StartLine = new Line(startLineA, startLineB);

            for (int i = 0; i < data.WaypointStarts.Count; i++)
            {
                var pointA = LoadPoint(data.WaypointStarts[i]);
                var pointB = LoadPoint(data.WaypointEnds[i]);
                level.AIWaypoints.Add(new Line(pointA, pointB));
            }
        }

        private static void LoadMap(Level level, string data)
        {
            data = data.Trim();

            // It's important that we don't just use our iterator variable i as our tileindex
            // because of whitespaces throwing i out of sync with the current tileindex.
            int tileIndex = 0;
            for (int i = 0; i < data.Length; i++)
            {
                var curChar = data[i];
                if (char.IsWhiteSpace(curChar))
                {
                    continue;
                }
                if(!char.IsNumber(curChar))
                {
                    throw new LevelParseException($"Level map contains invalid characters.{Environment.NewLine}(Position: {i})");
                }
                int x = tileIndex % level.LevelSize.X;
                int y = tileIndex / level.LevelSize.X;
                level.Tiles[x, y] = int.Parse(curChar.ToString());
                tileIndex++;
            }
        }

        private static void LoadMetadataXml(XmlElement root, out (string Title, string Size, string Modes, string MaxPlayers, string StartLineStart, string StartLineEnd,
            List<string> WaypointStarts, List<string> WaypointEnds) data)
        {
            string title = "";
            string size = "";
            string modes = "";
            string maxPlayers = "";
            string startLineStart = "";
            string startLineEnd = "";
            List<string> waypointStarts = new List<string>();
            List<string> waypointEnds = new List<string>();

            // Load in all the data.
            foreach (XmlElement element in root.ChildNodes)
            {
                if(string.Equals(element.Name, "title", StringComparison.OrdinalIgnoreCase))
                {
                    title = element.InnerText;
                }
                else if(string.Equals(element.Name, "size", StringComparison.OrdinalIgnoreCase))
                {
                    size = element.InnerText;
                }
                else if (string.Equals(element.Name, "modes", StringComparison.OrdinalIgnoreCase))
                {
                    modes = element.InnerText;
                }
                else if (string.Equals(element.Name, "maxplayers", StringComparison.OrdinalIgnoreCase))
                {
                    maxPlayers = element.InnerText;
                }
                else if (string.Equals(element.Name, "startline", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (XmlElement lineElement in element.ChildNodes)
                    {
                        if (string.Equals(lineElement.Name, "start", StringComparison.OrdinalIgnoreCase))
                        {
                            startLineStart = lineElement.InnerText;
                        }
                        else if (string.Equals(lineElement.Name, "end", StringComparison.OrdinalIgnoreCase))
                        {
                            startLineEnd = lineElement.InnerText;
                        }
                    }
                }
                else if (string.Equals(element.Name, "waypoints", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (XmlElement lineElement in element.ChildNodes)
                    {
                        if (string.Equals(lineElement.Name, "start", StringComparison.OrdinalIgnoreCase))
                        {
                            waypointStarts.Add(lineElement.InnerText);
                        }
                        else if (string.Equals(lineElement.Name, "end", StringComparison.OrdinalIgnoreCase))
                        {
                            waypointEnds.Add(lineElement.InnerText);
                        }
                    }
                }
            }

            data = (title, size, modes, maxPlayers, startLineStart, startLineEnd, waypointStarts, waypointEnds);
        }

        private static void CheckNullMetadata((string Title, string Size, string Modes, string MaxPlayers, string StartLineStart, string StartLineEnd,
            List<string> WaypointStarts, List<string> WaypointEnds) data)
        {
            if (string.IsNullOrWhiteSpace(data.Title))
            {
                throw new LevelParseException("Level does not specify title.");
            }
            if (string.IsNullOrWhiteSpace(data.Size))
            {
                throw new LevelParseException("Level does not specify size.");
            }
            if (string.IsNullOrWhiteSpace(data.Modes))
            {
                throw new LevelParseException("Level does not specify available game modes.");
            }
            if (string.IsNullOrWhiteSpace(data.MaxPlayers))
            {
                throw new LevelParseException("Level does not specify maximum number of players.");
            }
            if (string.IsNullOrWhiteSpace(data.StartLineStart))
            {
                throw new LevelParseException("Level does not specify starting line start position.");
            }
            if (string.IsNullOrWhiteSpace(data.StartLineEnd))
            {
                throw new LevelParseException("Level does not specify starting line end position.");
            }
            if (data.WaypointStarts.Count == 0)
            {
                throw new LevelParseException("Level does not specify any AI waypoint starting positions.");
            }
            if (data.WaypointEnds.Count == 0)
            {
                throw new LevelParseException("Level does not specify any AI waypoint ending positions.");
            }
            if (data.WaypointStarts.Count != data.WaypointEnds.Count)
            {
                throw new LevelParseException("Level contains waypoint start/end count mismatch.");
            }
        }

        private static Point LoadPoint(string data)
        {
            // Hack to let us re-use this function for the level size also, which is in the form #x#.
            data = data.ToLower().Replace('x', ',');
            if(!data.Contains(","))
            {
                throw new LevelParseException($"Point must be in the format #,#.{Environment.NewLine}(Data: {data})");
            }

            var parts = data.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length != 2)
            {
                throw new LevelParseException($"Point was missing component. Points must be in the format #,#.{Environment.NewLine}(Data: {data})");
            }

            if(!int.TryParse(parts[0], out int x))
            {
                throw new LevelParseException($"Point has an invalid X component.{Environment.NewLine}(Data: {data})");
            }
            if (!int.TryParse(parts[1], out int y))
            {
                throw new LevelParseException($"Point has an invalid Y component.{Environment.NewLine}(Data: {data})");
            }

            return new Point(x, y);
        }
    }
}
