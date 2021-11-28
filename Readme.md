#WebSpa

A simple build for .Net Core Mvc that provides faster page transitions

## demo




## How to use

Add PartialAjax attribute to your Actions

```cs
[PartialAjax]
public IActionResult Index()
{
    TestModel testModel = new TestModel();
    testModel.Message = "Hello";
    ViewData["Title"] = "Home";
    return View(testModel);
}
```

Create a Partial View with the same name as the View you created by prefixing it with a "_" sign.

```
.
├── Views
├── Home                    
│   ├── Index.cshtml              
│   ├── _Index.cshtml              
│   ├── Privacy.cshtml             
│   ├── _Privacy.cshtml      
│   └── ...
└── ...
```

## Example Views

Index.cshtml
```html
@model TestModel
@{
    Html.RenderPartial("_Index", Model);
}

```

_Index.cshtml
```html

@model TestModel
<div class="text-center">
    <h1 class="display-4">@Model.Message</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
</div>
```