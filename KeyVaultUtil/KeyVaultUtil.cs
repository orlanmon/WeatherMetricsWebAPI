using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVaultUtilities
{
    public static class KeyVaultUtil
    {


        public static async Task<string> GetKeyVaultDatabaseConnection(IConfiguration configuration)
        {


            var ApplicationSettings = configuration.GetSection("AppSettings");
            string? KeyVaultUrl = null;
            string? KeyVaultSecret = null;

            string? DatabaseConnectionString = null;


            KeyVaultUrl = ApplicationSettings["KeyVaultUrl"];
            KeyVaultSecret = ApplicationSettings["KeyVaultSecret"];

            if (String.IsNullOrEmpty(KeyVaultUrl) || String.IsNullOrEmpty(KeyVaultSecret))
            {

                throw new Exception("AppSetting:KeyVaultUrl and AppSetting:KeyVaultSecret where not both supplied.");

            }


            try
            {

                // DefaultAzureCredential automatically searches for environment variables, 
                // managed identities, or your IDE logged-in user (VS / Azure CLI) to authenticate.


                try
                {

                    var client = new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());



                    // Asynchronously fetch the secret
                    KeyVaultSecret secret = await client.GetSecretAsync(KeyVaultSecret);

                    DatabaseConnectionString = secret.Value;


                }
                catch (Exception ex)
                {

                    throw new Exception($"Error retrieving secret: {ex.Message}", ex);


                }




            }
            catch (Exception ex)
            {


                throw new Exception($"KeyVaultUtil::GetKeyVaultDatabaseConnection Error: {ex.Message}", ex);



            }
            finally
            {


            }


            return DatabaseConnectionString;


        }



    }
}
