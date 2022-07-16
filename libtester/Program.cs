using static parser.TableDownloader;

var bookLinks = GetBookLinks("https://mtuci.ru/time-table/");
Console.WriteLine("Found {0} links", bookLinks.Count);
bookLinks.ForEach(x => Console.WriteLine(x));