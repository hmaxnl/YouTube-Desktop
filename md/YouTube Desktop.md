# YouTube Desktop
Desktop application for YouTube using 'web scraping'.
Using the API (V1), where the web client is running on. By making calls to YouTube and the API to extract the information/data that the application can process and let the user interact with.

There will be more features implemented when there is a basic, stable and functional application, some points that give an idea:
* Theming
* Custom functionality
* Integration with other apps/programs
* Be more lightweight & more responsive than YouTube's app and site (At leats i'm trying to)


## YouTube GUI
The main executable/app that is the GUI and will make use of the library.
* [[Bootstrapper]] class where the library is setup on first start.
* AvaloniaUI framework that handles all the UI related work. And make it possible to run this app on Windows, Linux and macOS. (At least it should!)



## YouTube Scrap
The library contains a custom implementation for YouTube that the UI side can handle, using some main classes like [[YouTubeUser]] and [[NetworkHandler]] that will contains some basic functionality.
