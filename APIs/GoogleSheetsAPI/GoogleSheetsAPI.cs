using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using MongoDb.API.Models;
using MongoDb.API.Services;
using MongoDB.Bson;

namespace GoogleSheetsAPI;

public class GoogleSheets
{
    private readonly BusinessProvider _businessProvider; 
    
    static readonly string[] Scopes = {SheetsService.Scope.Spreadsheets};
    static readonly string ApplicationName = "IdentityAngularAPI";
    static readonly string SpreadsheetId = "1Ci0TBlU1anYDbh2lQDX1SjEdGVFtEeMuYs0-3KO6wPA";
    private static readonly string sheet = "Tickers";
    private static SheetsService service;
    
    static void XlsInitialization()
    {
        GoogleCredential credential;
        using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
    }

    public async Task<IEnumerable<TickerModel>> PopulateFromXls()
    {
        XlsInitialization();
            
        var range = $"{sheet}!A:D";
        var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);

        var response = request.Execute();
        var values = response.Values;

        List<TickerModel> results = new List<TickerModel>();

        if (values != null && values.Count > 0)
        {
            foreach (var row in values)
            {
                var result = new TickerModel
                {
                    Id = ObjectId.Parse(row[0].ToString()),
                    Company = row[1].ToString(),
                    Value = (decimal)row[2],
                    Date = DateTime.Parse(row[3].ToString()),
                };
                    
                await _businessProvider.PostNewTicker(result);
            }
        }
            
        return results;
    }

    public void ReadXlsEntries()
    {
        var range = $"{sheet}!A:D";
        var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);

        var response = request.Execute();
        var values = response.Values;
        if (values != null && values.Count > 0)
        {
            foreach (var row in values)
            {
                Console.Write("{0} {1} | {2} {3}", row[0], row[1], row[2], row[3]);
                // here i should read from the xls and populate the mongoDb with the business provider
            }
        }
    }

    public void CreateXlsEntry(TickerModel result)
    {
        XlsInitialization();
        var range = $"{sheet}!A:D";
        var valueRange = new ValueRange();

        var objectList = new List<object>() { result.Id, result.Company, result.Value, result.Date.ToString() }; // have to add every column item as they'll compose the object to be inserted
        valueRange.Values = new List<IList<object>> {objectList};

        var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
        appendRequest.ValueInputOption =
            SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        var appendResponse = appendRequest.Execute();
    }

    public void UpdateXlsEntry()
    {
        var range = ""; //have to input the row to modify from the interface!!!!
        var valueRange = new ValueRange();

        var objectList = new List<object>() {"elements to modify"}; // here we also need to insert the cell to modify (in the RANGE param) with the new value
        valueRange.Values = new List<IList<object>> {objectList};

        var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        var updateResponse = updateRequest.Execute();
    }
}