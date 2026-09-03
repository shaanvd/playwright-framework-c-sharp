using SauceDemo.Playwright.CSharp.Models;
using SauceDemo.Playwright.CSharp.Pages;
using NUnit.Framework;

namespace SauceDemo.Playwright.CSharp.Tests;

[TestFixture]
[Category("Regression")]

public sealed class NewTests : BaseTest
{
    [Test, Category("TC-NEW-101")]
    public async Task AddItemToCart()
    {
        await LoginAsAsync();
        await InventoryPage.AssertLoadedAsync();
        await InventoryPage.AddProductAsync("Sauce Labs Fleece Jacket");
        await InventoryPage.AssertCartCountAsync(1);
    }

    [Test, Category("TC-NEW-102")]
    public async Task RemoveItemFromCart()
    {
        await LoginAsAsync();
        await InventoryPage.AssertLoadedAsync();
        await InventoryPage.AddProductAsync("Sauce Labs Fleece Jacket");
        await InventoryPage.AddProductAsync("Sauce Labs Backpack");
        await InventoryPage.AssertCartCountAsync(2);
        await InventoryPage.RemoveProductAsync("Sauce Labs Backpack");
        await InventoryPage.AssertCartCountAsync(1);
    }

    [Test, Category("TC-NEW-103")]
    public async Task UserCanLogin()
    {
        await LoginAsAsync();
        await InventoryPage.OpenMenuAsync();
    }

    [Test, Category("TC-NEW-104")]
    public async Task UserCanLogout()
    {
        await LoginAsAsync();
        await InventoryPage.OpenMenuAsync();
        await Menu.LogoutAsync();
        await LoginPage.AssertLoadedAsync();
    }

    [Test, Category("TC-NEW-105")]
    public async Task InvaidPassword()
    {
        await LoginPage.OpenAsync(Settings.BaseUrl);
        await LoginPage.LoginAsync("standard_user", "wrong_password"); //put in config file
        await LoginPage.AssertErrorContainsAsync("Username and password do not match");
    }

    [Test, Category("TC-NEW-106")]
    public async Task ForcedFail()
    {
        await LoginPage.OpenAsync(Settings.BaseUrl);
        await LoginAsAsync();
        await LoginPage.AssertErrorContainsAsync("Username and password do not match");
    }
}