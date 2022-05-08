#Library
# YouTube User
One of the main classes used in the library, when the app is started it will load or create a user and fetch data from YouTube.

## Constructor
On creating the class it will setup some sub classes that it need to function, like the [[NetworkHandler]] class and validation of the cookies.

### Cookies
On construction of the class one of the parameters is a cookie container, this container can be used like in a web browser. Mostly this will be used if a user will login via a intergrated web browser ([[CEF]]), those cookies will be used in the [[NetworkHandler]] to send them with the requests to YouTube.
If null it creates a new cookie container!

### Proxy
The other paramter is a proxy that the [[NetworkHandler]] class will use to pass the network traffic through.
If null it does not use a proxy for the network traffic.

## Initial response
On constructing and setting everything up the class will fetch data from YouTube, this data is needed for further funtioning of the application. This data is the HTML web page of the YouTube web client, in there is some JSON data that will be extracted and used to build the user class.

## Authentication
If a user is logged in we extract a hash from the SAPISID cookie to create a authentication header, this is done in the [[UserAuthentication]] class.