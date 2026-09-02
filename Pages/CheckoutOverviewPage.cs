using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace SauceDemo.Playwright.CSharp.Pages;

public sealed class CheckoutOverviewPage : BasePage
{
    private ILocator Items => Page.Locator(".cart_item");
    private ILocator ItemTotal => Page.Locator(".summary_subtotal_label");
    private ILocator Tax => Page.Locator(".summary_tax_label");
    private ILocator Total => Page.Locator(".summary_total_label");
    private ILocator FinishButton => Page.Locator("#finish");
    private ILocator CancelButton => Page.Locator("#cancel");

    public CheckoutOverviewPage(IPage page) : base(page) { }

    public Task AssertContainsAsync(string productName) =>
        Expect(Items.Filter(new() { HasText = productName })).ToHaveCountAsync(1);

    public Task<string> ItemTotalTextAsync() => ItemTotal.InnerTextAsync();
    public Task<string> TaxTextAsync() => Tax.InnerTextAsync();
    public Task<string> TotalTextAsync() => Total.InnerTextAsync();

    public async Task FinishAsync() => await ClickAsync(FinishButton, "Finish order");
    public async Task CancelAsync() => await ClickAsync(CancelButton, "Cancel order");

    public static decimal ParseMoney(string text) =>
        decimal.Parse(
            System.Text.RegularExpressions.Regex.Match(text, @"\d+\.\d{2}").Value,
            System.Globalization.CultureInfo.InvariantCulture);
}