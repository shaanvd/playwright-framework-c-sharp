using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace SauceDemo.Playwright.CSharp.Pages;

public sealed class InventoryPage : BasePage
{
    public const string Route = "inventory.html";

    private ILocator InventoryContainer => Page.Locator("#inventory_container");
    private ILocator Items => Page.Locator(".inventory_item");
    private ILocator CartLink => Page.Locator(".shopping_cart_link");
    private ILocator CartBadge => Page.Locator(".shopping_cart_badge");
    private ILocator Sort => Page.Locator("[data-test='product-sort-container']");
    private ILocator MenuButton => Page.Locator("#react-burger-menu-btn");

    public InventoryPage(IPage page) : base(page) { }

    public Task AssertLoadedAsync() =>
        Expect(InventoryContainer.First).ToBeVisibleAsync();

    public Task<int> ProductCountAsync() => Items.CountAsync();

    public async Task AddProductAsync(string productName)
    {
        var item = Items.Filter(new() { HasText = productName });
        await ClickAsync(item.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }), $"Add {productName}");
    }

    public async Task RemoveProductAsync(string productName)
    {
        var item = Items.Filter(new() { HasText = productName });
        await ClickAsync(item.GetByRole(AriaRole.Button, new() { Name = "Remove" }), $"Remove {productName}");
    }

    public Task AssertCartCountAsync(int count) =>
        Expect(CartBadge).ToHaveTextAsync(count.ToString());

    public Task AssertCartBadgeAbsentAsync() =>
        Expect(CartBadge).ToHaveCountAsync(0);

    public async Task OpenCartAsync() =>
        await ClickAsync(CartLink, "Shopping cart");

    public async Task OpenProductAsync(string productName)
    {
        var item = Items.Filter(new() { HasText = productName });
        await ClickAsync(item.Locator(".inventory_item_name"), $"Product {productName}");
    }

    public async Task SortAsync(string value)
    {
        await Sort.SelectOptionAsync(value);
    }

    public async Task<IReadOnlyList<string>> ProductNamesAsync() =>
        await Items.Locator(".inventory_item_name").AllInnerTextsAsync();

    public async Task<IReadOnlyList<decimal>> ProductPricesAsync()
    {
        var texts = await Items.Locator(".inventory_item_price").AllInnerTextsAsync();
        return texts.Select(t => decimal.Parse(t.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture)).ToList();
    }

    public async Task OpenMenuAsync() =>
        await ClickAsync(MenuButton, "Open menu");
}