package com.bablabd.doctorswindow;

import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;



import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class ProfilePicture extends Activity {

	private static String insertImageLink = "http://bablabd.com/docwin/insertimagelink.php";
	String name;
	String phonenumber;
	Uri flnam;
	private TextView messageText;
	private Button uploadButton, btnselectpic;
	private ImageView imageview;
	private int serverResponseCode = 0;
	private ProgressDialog dialog = null;

	public String upLoadServerUri = null;
	public String imagepath = null;
	String imageName;
	String provider;
	protected String latitude, longitude;
	protected boolean gps_enabled, network_enabled;
	Double lati;
	Double longi;
	int success = 1;
	String fileName;
	String du="g";
	String docsEmail;
	// Progress Dialog
	private ProgressDialog pDialog;

	PHPRequest phpRequest = new PHPRequest();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.profilepic);
		

		int ds;
		try {
			FileInputStream fin = openFileInput("doctorsemal");

			int c;

			String temp = "";
			while ((c = fin.read()) != -1) {
				temp = temp + Character.toString((char) c);
			}

			du = temp;

		} catch (Exception ex) {
		}
		docsEmail=du;
		imageview = (ImageView) findViewById(R.id.profilIimageView);
		btnselectpic = (Button) findViewById(R.id.button_selectpic);
		uploadButton = (Button) findViewById(R.id.ProfileSendButton);
		upLoadServerUri = "http://bablabd.com/docwin/image/UploadToServer.php";
		ImageView img = new ImageView(this);
		uploadButton.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View view) {
				// creating new product in background thread
				success = 1;
				new uploadAsync().execute();

			}
		});

		btnselectpic.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent intent = new Intent();
				intent.setType("image/*");
				intent.setAction(Intent.ACTION_GET_CONTENT);
				startActivityForResult(
						Intent.createChooser(intent, "Complete action using"),
						1);
			}
		});

	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {

		if (requestCode == 1 && resultCode == RESULT_OK) {
			// Bitmap photo = (Bitmap) data.getData().getPath();

			Uri selectedImageUri = data.getData();
			imagepath = getPath(selectedImageUri);
			Bitmap bitmap = BitmapFactory.decodeFile(imagepath);
			imageview.setImageBitmap(bitmap);
			flnam = selectedImageUri;
//			Toast.makeText(ProfilePicture.this, getPathsss(imagepath),
//					Toast.LENGTH_SHORT).show();

			// imageName = (String) imageview.getTag();
			// messageText.setText("Uploading file path:" +imagepath);

		}
	}

	public String getPathsss(String uri) {

		String path = uri;
		String lastword = path.substring(path.lastIndexOf('/') + 1);
		return lastword;
	}

	public String getPath(Uri uri) {
		String[] projection = { MediaStore.Images.Media.DATA };
		Cursor cursor = managedQuery(uri, projection, null, null, null);
		int column_index = cursor
				.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
		cursor.moveToFirst();
		return cursor.getString(column_index);
	}

	public int uploadFile(String sourceFileUri) {

		fileName = sourceFileUri;

		HttpURLConnection conn = null;
		DataOutputStream dos = null;
		String lineEnd = "\r\n";
		String twoHyphens = "--";
		String boundary = "*****";
		int bytesRead, bytesAvailable, bufferSize;
		byte[] buffer;
		int maxBufferSize = 1 * 1024 * 1024;
		File sourceFile = new File(sourceFileUri);

		if (!sourceFile.isFile()) {

			Log.e("uploadFile", "Source File not exist :" + imagepath);

			runOnUiThread(new Runnable() {
				public void run() {
					// messageText.setText("Source File not exist :"+
					// imagepath);
				}
			});

			return 0;

		} else {
			try {

				// open a URL connection to the Servlet
				FileInputStream fileInputStream = new FileInputStream(
						sourceFile);
				URL url = new URL(upLoadServerUri);

				// Open a HTTP connection to the URL
				conn = (HttpURLConnection) url.openConnection();
				conn.setDoInput(true); // Allow Inputs
				conn.setDoOutput(true); // Allow Outputs
				conn.setUseCaches(false); // Don't use a Cached Copy
				conn.setRequestMethod("POST");
				conn.setRequestProperty("Connection", "Keep-Alive");
				conn.setRequestProperty("ENCTYPE", "multipart/form-data");
				conn.setRequestProperty("Content-Type",
						"multipart/form-data;boundary=" + boundary);
				conn.setRequestProperty("uploaded_file", fileName);

				dos = new DataOutputStream(conn.getOutputStream());

				dos.writeBytes(twoHyphens + boundary + lineEnd);
				dos.writeBytes("Content-Disposition: form-data; name=\"uploaded_file\";filename=\""
						+ fileName + "\"" + lineEnd);

				dos.writeBytes(lineEnd);

				// create a buffer of maximum size
				bytesAvailable = fileInputStream.available();

				bufferSize = Math.min(bytesAvailable, maxBufferSize);
				buffer = new byte[bufferSize];

				// read file and write it into form...
				bytesRead = fileInputStream.read(buffer, 0, bufferSize);

				while (bytesRead > 0) {

					dos.write(buffer, 0, bufferSize);
					bytesAvailable = fileInputStream.available();
					bufferSize = Math.min(bytesAvailable, maxBufferSize);
					bytesRead = fileInputStream.read(buffer, 0, bufferSize);

				}

				// send multipart form data necesssary after file data...
				dos.writeBytes(lineEnd);
				dos.writeBytes(twoHyphens + boundary + twoHyphens + lineEnd);

				// Responses from the server (code and message)
				serverResponseCode = conn.getResponseCode();
				String serverResponseMessage = conn.getResponseMessage();

				Log.i("uploadFile", "HTTP Response is : "
						+ serverResponseMessage + ": " + serverResponseCode);

				if (serverResponseCode == 200) {

					runOnUiThread(new Runnable() {
						public void run() {
							String msg = "File Upload Completed.\n\n See uploaded file here : \n\n"
									+ " F:/wamp/wamp/www/uploads";
							// messageText.setText(msg);
							// Toast.makeText(Profile.this,
							// "File Upload Complete.",
							// Toast.LENGTH_SHORT).show();
						}
					});
				}

				// close the streams //
				fileInputStream.close();
				dos.flush();
				dos.close();

			} catch (MalformedURLException ex) {

				ex.printStackTrace();

				runOnUiThread(new Runnable() {
					public void run() {
						// messageText.setText("MalformedURLException Exception : check script url.");
						Toast.makeText(ProfilePicture.this,
								"MalformedURLException", Toast.LENGTH_SHORT)
								.show();
					}
				});

				Log.e("Upload file to server", "error: " + ex.getMessage(), ex);
			} catch (Exception e) {

				e.printStackTrace();

				runOnUiThread(new Runnable() {
					public void run() {
						// messageText.setText("Got Exception : see logcat ");
						Toast.makeText(ProfilePicture.this,
								"Got Exception : see logcat ",
								Toast.LENGTH_SHORT).show();
					}
				});
				Log.e("Upload file to server Exception",
						"Exception : " + e.getMessage(), e);
			}

			return serverResponseCode;

		} // End else block
	}
	 public class uploadAsync extends AsyncTask<String, String, String> {
			
			@Override
			protected void onCancelled() {
				// TODO Auto-generated method stub
				super.onCancelled();
			}
			
			/**
			 * Before starting background thread Show Progress Dialog
			 * */
			@Override
			protected void onPreExecute() {
				super.onPreExecute();
				pDialog = new ProgressDialog(ProfilePicture.this);
				pDialog.setMessage("Data Sending...");
				pDialog.setIndeterminate(false);
				pDialog.setCancelable(true);
				pDialog.show();
				
			}

			/**
			 * Creating product
			 * */
			protected String doInBackground(String... args) {
				
				uploadFile(imagepath);
				
				List<NameValuePair> params = new ArrayList<NameValuePair>();
				params.add(new BasicNameValuePair("email",docsEmail));				
				params.add(new BasicNameValuePair("filename",getPathsss(imagepath)));
				

				// getting JSON Object
				// Note that create product url accepts POST method
				JSONObject json = phpRequest.makeHttpRequest(insertImageLink,"POST", params);
				
				
				
				
				
				return null;
			}

			/**
			 * After completing background task Dismiss the progress dialog
			 * **/
			protected void onPostExecute(String file_url) {
				// dismiss the dialog once done
				//ds=Integer.parseInt(dudhvatScore);
				
				pDialog.dismiss();
				Toast.makeText(getApplicationContext(), "Image has been uploaded", Toast.LENGTH_LONG).show();
				
				
			}

		}
}
