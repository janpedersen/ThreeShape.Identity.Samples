OAuth for Apps: Sample Desktop Application for Windows


Introduction
------------

When doing an OAuth 2.0 Authorization flow in a native application, it is 
important to follow best practices, which require using the browser (and not 
an embedded browser).

This sample demonstrates how you can open the user's browser with your OAuth 2.0
authorization request (where they might already be logged in!), have them
complete the consent, receive the Authorization Code using a local loopback
socket, and exchanging that code for authorization tokens.

This sample is an adaption of [this Google sample](https://github.com/googlesamples/oauth-apps-for-windows/tree/master/OAuthDesktopApp)
With the 3Shape Identity backend.

Projects:
---------
- Console App
- WPF app
- Razor Pages App
- Web APis

**Note:** The OauthRazorWebApp2 is not recommanded to use. 

Getting Started
--------------

1. Open the solution file: `Samples.sln`
2. Set one of the project as the startup app
3. Run the app.
4. When the app starts, tap "Sign in with 3Shape" and go through the flow.

OAuth Documentation
--------------------

The protocols referenced in this sample are documented here:

- [OAuth 2.0](https://oauth.net/2/)

Using your own credentials
--------------------------

The Sample comes backed with some demo client credentials, which are fine for
testing, but make sure you use your own credentials before releasing any app,
or sharing it with friends.

1. Contact the Identity team for credentials
2. Create a new OAuth 2.0 client, select `Other`
3. Copy the client id and client secret, and replace the values supplied in this
   sample.



Advanced Reading
----------------

The protocols and best practices used and implemented in these samples are
defined by RFCs. These expert-level documents detail how the protocols work,
and explain the reasoning behind many decisions, such as why we send a
`code_challenge` on the Authorization Request for a native app.

- [Internet-Draft: OAuth 2.0 for Native Apps BCP](https://tools.ietf.org/html/draft-ietf-oauth-native-apps)
- [RFC6749: OAuth 2.0](https://tools.ietf.org/html/rfc6749)
- [RFC6750: OAuth 2.0 Bearer Token Usage](https://tools.ietf.org/html/rfc6750)
- [RFC6819: OAuth 2.0 Threat Model and Security Considerations](https://tools.ietf.org/html/rfc6819)
- [RFC7636: OAuth 2.0 PKCE](https://tools.ietf.org/html/rfc7636)

