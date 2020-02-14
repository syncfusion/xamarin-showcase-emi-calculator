using Android.Content;
using Java.IO;
using System;
using System.IO;
using Xamarin.Forms;
using LoanCalculator.Droid;
using System.Threading.Tasks;
using Android.Widget;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;

[assembly: Dependency(typeof(SaveAndroid))]

namespace LoanCalculator.Droid
{
    public class SaveAndroid : ISave
    {
        public void Save(string fileName, String contentType, MemoryStream stream)
        {
            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Android.App.Activity)MainActivity.Context, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
            }

            string exception = string.Empty;
            string root = null;
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            if (file.Exists() && contentType != "application/html")
            {
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.AddFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.MultipleTask);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".provider", file);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Android.App.Application.Context.StartActivity(intent);
            }
        }

        public Task SaveWindows(string filename, string contentType, MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        private void AlertAction()
        {
            Toast.MakeText(Android.App.Application.Context, "File has been successfully exported", ToastLength.Long).Show();
        }

    }
}