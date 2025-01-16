using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ChovEvidApi.Dto;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;

namespace ChovEvid.Repositories
{
    public class BreedingStationRepository : IBreedingStationRepository
    {
        private readonly string _connectionString;

        public BreedingStationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<BreedingStationDto> GetAll()
        {
            var breedingStations = new List<BreedingStationDto>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT chs.reg_number, chs.name, p.first_name || ' ' || p.last_name, count(d.id) as dog_count, chs.created, chs.location " +
                                            "FROM chovevid_breeding_station chs " +
                                            "JOIN chovevid_person p on(p.id=chs.id_owner) " +
                                            "JOIN chovevid_dog d on(d.id_breeding_station = chs.id) " +
                                            "GROUP BY chs.reg_number, chs.name, p.first_name || ' ' || p.last_name, chs.created, chs.location " +
                                            "ORDER BY chs.reg_number";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            breedingStations.Add(new BreedingStationDto
                            {
                                RegNumber = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Owner = reader.GetString(2),
                                DogCount = reader.GetInt32(3),
                                Created = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                                Location = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return breedingStations;
        }

        public void GenerateBreedingStationDoc(IEnumerable<BreedingStationDto> breedingStations, string filePath)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                // Create main document part
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                // Add title
                Paragraph titleParagraph = new Paragraph();
                Run titleRun = new Run();
                titleRun.Append(new Text("Breeding Stations Report"));
                titleParagraph.Append(titleRun);
                titleParagraph.ParagraphProperties = new ParagraphProperties
                {
                    Justification = new Justification { Val = JustificationValues.Center },
                    SpacingBetweenLines = new SpacingBetweenLines { After = "200" }
                };
                body.Append(titleParagraph);

                // Create table
                Table table = new Table();

                // Add table styles
                TableProperties tableProperties = new TableProperties(
                    new TableBorders(
                        new TopBorder { Val = BorderValues.Single, Size = 6 },
                        new BottomBorder { Val = BorderValues.Single, Size = 6 },
                        new LeftBorder { Val = BorderValues.Single, Size = 6 },
                        new RightBorder { Val = BorderValues.Single, Size = 6 },
                        new InsideHorizontalBorder { Val = BorderValues.Single, Size = 6 },
                        new InsideVerticalBorder { Val = BorderValues.Single, Size = 6 }
                    )
                );
                table.AppendChild(tableProperties);

                // Add table header row
                TableRow headerRow = new TableRow();
                string[] headers = { "RegNumber", "Name", "Owner", "DogCount", "Created", "Location" };
                foreach (var header in headers)
                {
                    TableCell cell = new TableCell(new Paragraph(new Run(new Text(header))));
                    cell.TableCellProperties = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2400" });
                    headerRow.Append(cell);
                }
                table.Append(headerRow);

                // Populate table with data
                foreach (var station in breedingStations)
                {
                    TableRow dataRow = new TableRow();
                    dataRow.Append(
                        CreateTextCell(station.RegNumber.ToString()),
                        CreateTextCell(station.Name),
                        CreateTextCell(station.Owner),
                        CreateTextCell(station.DogCount.ToString()),
                        CreateTextCell(station.Created?.ToString("yyyy-MM-dd") ?? "N/A"),
                        CreateTextCell(station.Location ?? "Unknown")
                    );
                    table.Append(dataRow);
                }

                body.Append(table);

                // Add body to document
                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
        }

        private TableCell CreateTextCell(string text)
        {
            TableCell cell = new TableCell(new Paragraph(new Run(new Text(text))));
            cell.TableCellProperties = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2400" });
            return cell;
        }
    }
}
