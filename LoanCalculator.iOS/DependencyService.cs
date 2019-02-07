using Foundation;
using LoanCalculator.iOS;
using QuickLook;
using System;
using System.IO;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveIOS))]

namespace LoanCalculator.iOS
{
    public class SaveIOS : ISave
    {
        UIViewController currentController;
        public void Save(string filename, string contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, filename);
            try
            {
                FileStream fileStream = File.Open(filePath, FileMode.Create);
                stream.Position = 0;
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            if (contentType == "application/html" || exception != string.Empty)
                return;
            UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (currentController.PresentedViewController != null)
                currentController = currentController.PresentedViewController;
            UIView currentView = currentController.View;

            QuickLook.QLPreviewController qlPreview = new QuickLook.QLPreviewController();
            QuickLook.QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
            qlPreview.DataSource = new PreviewControllerDS(item);

            //UIViewController uiView = currentView as UIViewController;

            currentController.PresentViewController((UIViewController)qlPreview, true, (Action)null);
        }

        public Task SaveWindows(string filename, string contentType, MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        private void AlertAction()
        {
            var alert = UIAlertController.Create("Loan Calculator", "File has been successfully exported", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            currentController.PresentViewController(alert, true, null);
        }
    }

    public class PreviewControllerDS : QLPreviewControllerDataSource
    {
        private readonly QLPreviewItem item;

        public PreviewControllerDS(QLPreviewItem item)
        {
            this.item = item;
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return (nint)1;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return item;
        }
    }

    public class QLPreviewItemFileSystem : QLPreviewItem
    {
        private readonly string fileName, filePath;

        public QLPreviewItemFileSystem(string fileName, string filePath)
        {
            this.fileName = fileName;
            this.filePath = filePath;
        }

        public override string ItemTitle
        {
            get
            {
                return fileName;
            }
        }

        public override NSUrl ItemUrl
        {
            get
            {
                return NSUrl.FromFilename(filePath);
            }
        }
    }

    public class QLPreviewItemBundle : QLPreviewItem
    {
        private readonly string fileName, filePath;

        public QLPreviewItemBundle(string fileName, string filePath)
        {
            this.fileName = fileName;
            this.filePath = filePath;
        }

        public override string ItemTitle
        {
            get
            {
                return fileName;
            }
        }

        public override NSUrl ItemUrl
        {
            get
            {
                var documents = NSBundle.MainBundle.BundlePath;
                var lib = Path.Combine(documents, filePath);
                var url = NSUrl.FromFilename(lib);
                return url;
            }
        }
    }
}