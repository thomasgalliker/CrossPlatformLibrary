using System.Linq;
using System.Text;
using Android.Content;
using Android.Content.PM;
using Android.Util;
using Java.Security;
using Java.Util;
using Base64 = Android.Util.Base64;

namespace CrossPlatformLibrary.Tools
{
    /// <summary>
    ///     AppSignatureHelper calculates the current app's hash key.
    /// 
    ///     Original Source:
    ///     https://github.com/googlearchive/android-credentials/blob/master/sms-verification/android/app/src/main/java/com/google/samples/smartlock/sms_verify/AppSignatureHelper.java
    /// 
    ///     C# Translations:
    ///     https://github.com/search?l=C%23&q=AppHashKeyHelper&type=Code
    /// </summary>
    public static class AppSignatureHelper
    {
        private const string HashType = "SHA-256";
        private const int NumberOfHashedBytes = 9;
        private const int NumberOfBase64Chars = 11;

        /// <summary>
        ///     Retrieve the app signed package signature
        ///     known as 'signed keystore file hex string'.
        /// </summary>
        private static string GetPackageSignature(Context context)
        {
            var packageManager = context.PackageManager;
            var signatures = packageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.Signatures).Signatures;
            return signatures.First().ToCharsString();
        }

        /// <summary>
        ///     Gets the current app's hash key.
        /// </summary>
        /// <returns>The app hash key.</returns>
        /// <param name="context">Android app Context.</param>
        public static string GetAppHashKey(Context context)
        {
            string keystoreHexSignature = GetPackageSignature(context);

            var appInfo = context.PackageName + " " + keystoreHexSignature;
            try
            {
                var messageDigest = MessageDigest.GetInstance(HashType);
                messageDigest.Update(Encoding.UTF8.GetBytes(appInfo));
                byte[] hashSignature = messageDigest.Digest();

                hashSignature = Arrays.CopyOfRange(hashSignature, 0, NumberOfHashedBytes);
                var base64Hash = Base64.EncodeToString(hashSignature, Base64Flags.NoPadding | Base64Flags.NoWrap);
                base64Hash = base64Hash.Substring(0, NumberOfBase64Chars);

                return base64Hash;
            }
            catch (NoSuchAlgorithmException e)
            {
                return null;
            }
        }
    }
}