using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Views;
using Android.Provider;
using Android.Content;


using static Android.Graphics.Bitmap;
using Android.Net;
using Java.IO;
using System;
using Android.Graphics.Drawables;
using Android.Media;

namespace SaveViewAsBitMap
{
    [Activity(Label = "SaveViewAsBitMap", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);


            Button bt1 = FindViewById<Button>(Resource.Id.button1);

            View v = FindViewById<LinearLayout>(Resource.Id.myLinearLayout);

          
            bt1.Click += Bt1_Click;
          
        }

        private void Bt1_Click(object sender, System.EventArgs e)
        {
            View v = FindViewById<LinearLayout>(Resource.Id.myLinearLayout);
            Bitmap myBitMap = createViewBitmap(v);
            ImageView img = FindViewById<ImageView>(Resource.Id.imageView1);

            Drawable drawable = new BitmapDrawable(myBitMap);
            img.SetBackgroundDrawable(drawable);


            ImageView img2 = FindViewById<ImageView>(Resource.Id.imageView2);
            View v2 = FindViewById<LinearLayout>(Resource.Id.myLinearLayout);
            Bitmap myBitMap2 = createViewBitmap2(v);
            Drawable drawable2 = new BitmapDrawable(myBitMap2);
            img2.SetBackgroundDrawable(drawable2);
            // MediaStore.Images.Media.InsertImage(ContentResolver, myBitMap, "title", "description");
            saveImage(myBitMap);
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Java.IO.File myFile = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory + "/DCIM/Camera", "MikeBitMap4.jpg");
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(myFile);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
        }

        public Bitmap createViewBitmap(View v)
        {
            Bitmap bitmap = Bitmap.CreateBitmap(v.Width, v.Height,
                    Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);
            v.Draw(canvas);
            return bitmap;
        }

        public Bitmap createViewBitmap2(View v)
        {
            Bitmap bitmap = Bitmap.CreateBitmap(v.Width, v.Height,
                    Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);
            v.Draw(canvas);
            return zoomImg(bitmap, 370, 204);
        }
        public static Bitmap zoomImg(Bitmap bm, int newWidth, int newHeight)
        {
            // 获得图片的宽高   
            int width = bm.Width;
            int height = bm.Height;
            // 计算缩放比例   
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;
            // 取得想要缩放的matrix参数   
            Matrix matrix = new Matrix();
            matrix.PostScale(scaleWidth, scaleHeight);
            // 得到新的图片   www.2cto.com
            Bitmap newbm = Bitmap.CreateBitmap(bm, 0, 0, width, height, matrix, true);
            return newbm;
        }

        public static void saveImage(Bitmap bmp)
        {
           

            try
            {
                using (var os = new System.IO.FileStream(Android.OS.Environment.ExternalStorageDirectory + "/DCIM/Camera/MikeBitMap4.jpg", System.IO.FileMode.CreateNew))
                {
                    bmp.Compress(Bitmap.CompressFormat.Jpeg, 95, os);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}

