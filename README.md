# Consume Azure Function Through SSIS Package

This sample shows how SSIS can use Azure Function to do certain operations, like calculating based on Function response, updating the database table, etc.

While working with SSIS, we can use a Script task or Script Component to write the custom C# code. The Azure Function can be integrated through Script Task.
We can also use the project parameter to configure the Function URL based on the environment: Dev, Staging, and Production. 
It can be configured through environment variable on SSISDB (the place where we deploy our SSIS packages). So, it will be easy to change the environment for packages whenever it is required.

But for a calling Azure Function (with HttpClient) we have to reference two Assemblies: **System.Net.Http.Formatting.dll** which is used for media type formatting and **Newtonsoft.dll** for Json support.
The documentation recommends to install them in the Global Assembly Cache (GAC) and reference them from GAC. This is not always possible or simple to do. What now?

This sample I've used workaround for loading an assembly from an shared location. Solution is to register an **AppDomain.AssemblyResolve** event handler.
You can then drop the referenced assemblies in a shared location on the SSIS server, or use a package variable to refer to the location and pass that in to the Script Task or Script Component.

**Please use it only for your own or other certified assemblies, because this would be a security issue.**

Enjoy!

## Licence

Licenced under [MIT](http://opensource.org/licenses/mit-license.php).
Contact me on [LinkedIn](https://si.linkedin.com/in/matjazbravc).
