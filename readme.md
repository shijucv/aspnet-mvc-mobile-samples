# Project Liike
## Mobile Features of ASP.NET MVC

These samples demonstrate specific features of ASP.NET MVC related to targeting mobile devices.
Specifically, it focuses on the technical issue of how to deliver a _different set of markup to different devices_. 
It does not address the question of how to determine if doing so is the appropriate solution for your application.

The samples and supporting articles were authored by [Akira Inoue](https://github.com/chack411) of Microsoft Japan. Akira can also be found on [twitter](http://twitter.com/chack411).

* **Mvc3** demonstrates how to switch between a mobile view and a desktop view using ASP.NET MVC 3.
* **Mvc4** demonstrates how to switch views using new features in the forthcoming ASP.NET MVC 4.

## Running the Samples

These samples require Visual Studio 2010. Find out how to obtain a **free** copy of Visual Studio Web Developer Express at the official [ASP.NET site](http://www.asp.net/mvc).

You can also download a developer preview of [ASP.NET MVC 4](http://www.asp.net/mvc/mvc4) that can be installed side-by-side with ASP.NET MVC 3.

You'll want to install [NuGet](http://www.nuget.org/) in order to manage the dependencies.

**NOTE:** You will not see the link for switching views when you access the sample from a browser that
ASP.NET does not recognize as mobile. For example, you'll need to use something like the [Windows Phone Emulator](msdn.com/en-us/library/ff402563) to see the correct behavior. For more information on how ASP.NET determines browser capabilities [Browser Definition File Schema](http://msdn.microsoft.com/en-us/library/ms228122.aspx). It is likely that the default behavior will not be sufficient for many production applications.

## MOAR OSS!
Find out more about [Microsoft and Open Source](http://www.asp.net/mvc/open-source).

## Acknowledgments
Portions of this sample make use of the [work from Scott Hanselman and Peter Mournfield](http://www.hanselman.com/blog/NuGetPackageOfTheWeek10NewMobileViewEnginesForASPNETMVC3SpeccompatibleWithASPNETMVC4.aspx).