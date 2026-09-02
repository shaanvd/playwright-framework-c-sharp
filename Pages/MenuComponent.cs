using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace SauceDemo.Playwright.CSharp.Pages;

public sealed class MenuComponent : BasePage
{
    private ILocator MenuContainer => Page.Locator(".bm-menu-wrap");
    private ILocator AllItems => Page.Locator("#inventory_sidebar_link");
    private ILocator Reset => Page.Locator("#reset_sidebar_link");
    private ILocator Logout => Page.Locator("#logout_sidebar_link");
    private ILocator CloseButton => Page.Locator("#react-burger-cross-btn");

    public MenuComponent(IPage page) : base(page) { }

    public Task AssertOpenAsync() => Expect(MenuContainer).ToBeVisibleAsync();

    public async Task ResetAppStateAsync()
    {
        await Expect(Reset).ToBeVisibleAsync();
        await ClickAsync(Reset, "Reset app state");
    }

    public async Task LogoutAsync()
    {
        await Expect(Logout).ToBeVisibleAsync();
        await ClickAsync(Logout, "Logout");
    }

    public async Task AllItemsAsync()
    {
        await Expect(AllItems).ToBeVisibleAsync();
        await ClickAsync(AllItems, "All items");
    }

    public async Task CloseAsync() => await ClickAsync(CloseButton, "Close menu");
}