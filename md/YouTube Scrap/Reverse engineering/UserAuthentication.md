# User authentication
The user authentication class is only used to create the SAPISIDHASH authentication header, used for authentication the user to YouTube. The authentication header will be used in the [[NetworkHandler]] class.
Source: https://stackoverflow.com/a/32065323/9948300

## SAPISID
The user SAPISID is a cookie that will be hashed(SHA1) with a time epoch and server origin.