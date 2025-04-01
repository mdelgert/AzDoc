/*
https://learn.microsoft.com/en-us/azure/ai-services/document-intelligence/quickstarts/get-started-sdks-rest-api?view=doc-intel-4.0.0&pivots=programming-language-csharp

using Azure;
using Azure.AI.DocumentIntelligence;
using Microsoft.Extensions.Configuration;

// Build configuration
IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
    .Build();

// Retrieve endpoint and key from appsettings.json
string? endpoint = config["AzureDocumentIntelligence:Endpoint"];
string? key = config["AzureDocumentIntelligence:Key"];

// Throw exception if values are missing
if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("Azure Document Intelligence configuration is missing. Ensure 'Endpoint' and 'Key' are specified in 'AzureDocumentIntelligence' section of appsettings.json.");
}

AzureKeyCredential credential = new AzureKeyCredential(key);
DocumentIntelligenceClient client = new DocumentIntelligenceClient(new Uri(endpoint), credential);

//sample invoice document

Uri invoiceUri = new Uri("https://raw.githubusercontent.com/Azure-Samples/cognitive-services-REST-api-samples/master/curl/form-recognizer/sample-invoice.pdf");

AnalyzeDocumentContent content = new AnalyzeDocumentContent()
{
    UrlSource = invoiceUri
};

Operation<AnalyzeResult> operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-invoice", content);

AnalyzeResult result = operation.Value;

for (int i = 0; i < result.Documents.Count; i++)
{
    Console.WriteLine($"Document {i}:");

    AnalyzedDocument document = result.Documents[i];

    if (document.Fields.TryGetValue("VendorName", out DocumentField vendorNameField)
        && vendorNameField.Type == DocumentFieldType.String)
    {
        string vendorName = vendorNameField.ValueString;
        Console.WriteLine($"Vendor Name: '{vendorName}', with confidence {vendorNameField.Confidence}");
    }

    if (document.Fields.TryGetValue("CustomerName", out DocumentField customerNameField)
        && customerNameField.Type == DocumentFieldType.String)
    {
        string customerName = customerNameField.ValueString;
        Console.WriteLine($"Customer Name: '{customerName}', with confidence {customerNameField.Confidence}");
    }

    if (document.Fields.TryGetValue("Items", out DocumentField itemsField)
        && itemsField.Type == DocumentFieldType.List)
    {
        foreach (DocumentField itemField in itemsField.ValueList)
        {
            Console.WriteLine("Item:");

            if (itemField.Type == DocumentFieldType.Dictionary)
            {
                IReadOnlyDictionary<string, DocumentField> itemFields = itemField.ValueDictionary;

                if (itemFields.TryGetValue("Description", out DocumentField itemDescriptionField)
                    && itemDescriptionField.Type == DocumentFieldType.String)
                {
                    string itemDescription = itemDescriptionField.ValueString;
                    Console.WriteLine($"  Description: '{itemDescription}', with confidence {itemDescriptionField.Confidence}");
                }

                if (itemFields.TryGetValue("Amount", out DocumentField itemAmountField)
                    && itemAmountField.Type == DocumentFieldType.Currency)
                {
                    CurrencyValue itemAmount = itemAmountField.ValueCurrency;
                    Console.WriteLine($"  Amount: '{itemAmount.CurrencySymbol}{itemAmount.Amount}', with confidence {itemAmountField.Confidence}");
                }
            }
        }
    }

    if (document.Fields.TryGetValue("SubTotal", out DocumentField subTotalField)
        && subTotalField.Type == DocumentFieldType.Currency)
    {
        CurrencyValue subTotal = subTotalField.ValueCurrency;
        Console.WriteLine($"Sub Total: '{subTotal.CurrencySymbol}{subTotal.Amount}', with confidence {subTotalField.Confidence}");
    }

    if (document.Fields.TryGetValue("TotalTax", out DocumentField totalTaxField)
        && totalTaxField.Type == DocumentFieldType.Currency)
    {
        CurrencyValue totalTax = totalTaxField.ValueCurrency;
        Console.WriteLine($"Total Tax: '{totalTax.CurrencySymbol}{totalTax.Amount}', with confidence {totalTaxField.Confidence}");
    }

    if (document.Fields.TryGetValue("InvoiceTotal", out DocumentField invoiceTotalField)
        && invoiceTotalField.Type == DocumentFieldType.Currency)
    {
        CurrencyValue invoiceTotal = invoiceTotalField.ValueCurrency;
        Console.WriteLine($"Invoice Total: '{invoiceTotal.CurrencySymbol}{invoiceTotal.Amount}', with confidence {invoiceTotalField.Confidence}");
    }
}
*/