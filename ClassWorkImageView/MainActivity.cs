using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace ClassWorkImageView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ImageView ivPicture;
        private ImageButton ibCamera;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitializeViews();
        }
 
        private void InitializeViews()
        {
            ivPicture = FindViewById<ImageView>(Resource.Id.ivPicture);
            ibCamera = FindViewById<ImageButton>(Resource.Id.ibCamera);

            ibCamera.Click += ibCamera_click;
        }

        private void ibCamera_click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, 1);
            builder.SetTitle("Where you want to go?");
            builder.SetMessage("");
            builder.SetPositiveButton("Gallery", (c, ev) => GalleryIntent());
            builder.SetNegativeButton("Camera", (c, ev) => CameraIntent());
            builder.Show();
        }

        private void CameraIntent()
        {
            Intent takePicture = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(takePicture, 0);
        }

        private void GalleryIntent()
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent, 1);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 0)
            {
                if (resultCode == Result.Ok)
                {
                    Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                    ivPicture.SetImageBitmap(bitmap);
                }
            }

            else
            {
                if (resultCode == Result.Ok)
                {
                    Android.Net.Uri uri = data.Data;

                    ImageDecoder.Source source = ImageDecoder.CreateSource(ContentResolver, uri);
                    Bitmap bitmap = ImageDecoder.DecodeBitmap(source);
                    ivPicture.SetImageBitmap(bitmap);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}