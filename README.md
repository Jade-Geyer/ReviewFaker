# ReviewFaker - Technical Sample

This project was a technical test for a senior dev position in which I was asked to build a web service that performed a specific task of making fake amazon reviews. It sounds like I got tricked into working for scammers, but it was a legitimate company.

This project is self contained and should be able to run without further configuration. Open .../AmazonReviewFaker/AmazonReviewFaker.sln in Visual Studio 2022. The project uses .net 7 and entity framework for an in-memory database. The main project can be run as either https or IIS Express. Once initialization completes, a browser should open and make a sample call to the endpoint. Library imports/installs may be necessecary.

Very basic unit tests are setup using xunit and can be run through test explorer.
