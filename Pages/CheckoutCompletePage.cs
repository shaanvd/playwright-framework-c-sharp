using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace SauceDemo.Playwright.CSharp.Pages;

public sealed class CheckoutCompletePage : BasePage
{
    private ILocator CompleteHeader => Page.Locator(".complete-header");
    private ILocator BackHome => Page.Locator("#back-to-products");

    public CheckoutCompletePage(IPage page) : base(page) { }

    public Task AssertOrderCompleteAsync() =>
        Expect(CompleteHeader).ToHaveTextAsync("Thank you for your order!");

    public async Task BackHomeAsync() => await ClickAsync(BackHome, "Back home");
}