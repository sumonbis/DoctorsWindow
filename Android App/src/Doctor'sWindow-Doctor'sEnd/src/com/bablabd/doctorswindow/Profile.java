package com.bablabd.doctorswindow;

import java.io.FileInputStream;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

public class Profile extends Activity {
	int success=1;
	String doctoEmail;
	EditText userNameEditText;
	EditText emailEditText;
	EditText doctorsNameEditText;
	EditText qualificationEditText;
	Spinner specialitySpinner;
	EditText designationEditText;
	EditText instituteEditText;
	EditText addressEditText;
	EditText phoneNoEditText;
	EditText mobileNoEditText;
	EditText visitingTimeEditText;
	EditText noOfPatientEditText;
	Button updateBtn;
	private ProgressDialog pDialog;
	String speciality;
	String[] specialities = { "Medicine","Audiologists",
			"Allergist",
			"Andrologists",
			"Anesthesiologists",
			"Cardiologist",
			"Child Specialist",
			"Dentist",
			"Dermatologists",
			"Endocrinologists",
			"Epidemiologists",
			"Family Practician",
			"Gastroenterologists",
			"Gynecologists",
			"Hematologists",
			"Hepatologists",
			"Immunologists",
			"Infectious Disease Specialists",
			"Internal Medicine Specialists",
			"Internists",
			"Medical Geneticist",
			"Microbiologists",
			"Neonatologist",
			"Nephrologist",
			"Neurologist",
			"Neurosurgeons",
			"Obstetrician",
			"Oncologist",
			"Ophthalmologist",
			"Orthopedic Surgeons",
			"ENT specialists",
			"Perinatologist",
			"Paleopathologist",
			"Parasitologist" };
	String doctorsName;
	String qualification;

	String designation;
	String institute;
	String address;
	String phoneNo;
	String mobileNo;
	String visitingTime;
	String noOfPatient;
	String username;
	String email;
	
		
	
	JSONObject jsonn ;
	PHPRequest jsonParser = new PHPRequest();
	

	// url to create new product
	private static String updateUrl = "http://bablabd.com/docwin/updateprofile.php";
	private static String doctorsInfoUrl = "http://bablabd.com/docwin/doctorsinfo.php";
String du="g";
	// JSON Node names
	public static final String TAG_SUCCESS = "success";
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.doctor_profile);
		
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
		doctoEmail=du;
		doctorsNameEditText=(EditText)findViewById(R.id.nameEditText);
		qualificationEditText=(EditText)findViewById(R.id.qualificationEditText);
		specialitySpinner=(Spinner)findViewById(R.id.specialitySpinner);
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
				android.R.layout.simple_spinner_dropdown_item, specialities);		
		specialitySpinner.setAdapter(adapter);
		specialitySpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
			int count = 0;

			@Override
			public void onItemSelected(AdapterView<?> arg0, View arg1,
					int arg2, long arg3) {

				
					int position = specialitySpinner.getSelectedItemPosition();
					switch(position)
					{
					case 0:
						speciality="Medicine";
						break;
					case 1:
						speciality="Audiologists";
						break;
					case 2:
						speciality="Allergist";
						break;
					
					case 3:						
						speciality="Andrologists";
						break;
						
					case 4:
						speciality="Anesthesiologists";
						break;	
						
					case 5:
						speciality="Cardiologist";
						break;
					case 6:
						speciality="Child Specialist";
						break;
					case 7:						
						speciality="Dentist";
						break;
					case 8:						
						speciality="Dermatologists";
						break;
					case 9:
						speciality="Endocrinologists";
						break;
					case 10:
						speciality="Epidemiologists";
						break;
					case 11:	
						speciality="Family Practician";
						break;
					case 12:
						speciality="Gastroenterologists";
						break;
					case 13:
						speciality="Gynecologists";
						break;
					case 14:
						speciality="Hematologists";
						break;
					case 15:
						speciality="Hepatologists";
						break;
					case 16:
						speciality="Immunologists";
						break;
					case 17:
						speciality="Infectious Disease Specialists";
						break;
					case 18:
						speciality="Internal Medicine Specialists";
						break;
					case 19:
						speciality="Internists";
						break;
					case 20:
						speciality="Medical Geneticist";
						break;
					case 21:
						speciality="Microbiologists";
						break;
					case 22:
						speciality="Neonatologist";
						break;
					case 23:
						speciality="Nephrologist";
						break;
					case 24:
						speciality="Neurologist";
						break;
					case 25:
						speciality="Neurosurgeons";
						break;
					case 26:
						speciality="Obstetrician";
						break;
					case 27:
						speciality="Oncologist";
						break;
					case 28:
						speciality="Ophthalmologist";
						break;
					case 29:
						speciality="Orthopedic Surgeons";
						break;
					case 30:
						speciality="ENT specialists";
						break;
					case 31:
						speciality="Perinatologist";
						break;
					case 32:
						speciality="Paleopathologist";
						break;
					case 33:
						speciality="Parasitologist";
						break;
					
					
					}
					
					
			}
					
			
			
			
			@Override
						public void onNothingSelected(AdapterView<?> arg0) {
							// TODO Auto-generated method stub
						}
						
					});
		
		designationEditText=(EditText)findViewById(R.id.designatonEditText);
		instituteEditText=(EditText)findViewById(R.id.instituteEditText);
		addressEditText=(EditText)findViewById(R.id.addressEditText);
		phoneNoEditText=(EditText)findViewById(R.id.phoneNoEditText);
		mobileNoEditText=(EditText)findViewById(R.id.mobNoEditText);
		visitingTimeEditText=(EditText)findViewById(R.id.visitingTimeEditText);
		noOfPatientEditText=(EditText)findViewById(R.id.noOfPatientEditText);
		userNameEditText=(EditText)findViewById(R.id.userNameEditText);
		emailEditText=(EditText)findViewById(R.id.emailEditText);
		updateBtn=(Button)findViewById(R.id.updateButton);	
		new doctorsInfoAsync().execute();
		updateBtn.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				success=1;
				new updateAsync().execute();
			}
		});	

}
	 public class updateAsync extends AsyncTask<String, String, String> {
			
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
				pDialog = new ProgressDialog(Profile.this);
				pDialog.setMessage("Profile Updating...");
				pDialog.setIndeterminate(false);
				pDialog.setCancelable(true);
				pDialog.show();
				
			}

			/**
			 * Creating product
			 * */
			protected String doInBackground(String... args) {
				
doctorsName=doctorsNameEditText.getText().toString();
qualification=qualificationEditText.getText().toString();

 designation=designationEditText.getText().toString();
 institute=instituteEditText.getText().toString();
 address=addressEditText.getText().toString();
 phoneNo=phoneNoEditText.getText().toString();
 mobileNo=mobileNoEditText.getText().toString();
 visitingTime=visitingTimeEditText.getText().toString();
 noOfPatient=noOfPatientEditText.getText().toString();
				
				// Building Parameters
				List<NameValuePair> params = new ArrayList<NameValuePair>();
				params.add(new BasicNameValuePair("email",doctoEmail));	
				params.add(new BasicNameValuePair("name",doctorsName));
				params.add(new BasicNameValuePair("qualification",qualification));
				params.add(new BasicNameValuePair("speciality",speciality));
				params.add(new BasicNameValuePair("designation",designation));
				params.add(new BasicNameValuePair("institute",institute));
				params.add(new BasicNameValuePair("address",address));
				params.add(new BasicNameValuePair("phoneno",phoneNo));
				params.add(new BasicNameValuePair("mobileno",mobileNo));
				params.add(new BasicNameValuePair("visitingtime",visitingTime));
				params.add(new BasicNameValuePair("noofpatient",noOfPatient));			
				

				// getting JSON Object
				// Note that create product url accepts POST method
				JSONObject json = jsonParser.makeHttpRequest(updateUrl,"POST", params);				
				
				
				return null;
			}

			/**
			 * After completing background task Dismiss the progress dialog
			 * **/
			protected void onPostExecute(String file_url) {
				// dismiss the dialog once done
				//ds=Integer.parseInt(dudhvatScore);
				
				pDialog.dismiss();
				Toast.makeText(getApplicationContext(), "Profile Updated.",Toast.LENGTH_LONG).show();
				
			}	
	
	 }
	 
	 public class doctorsInfoAsync extends AsyncTask<String, String, String> {
			
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
				pDialog = new ProgressDialog(Profile.this);
				pDialog.setMessage("Loading...");
				pDialog.setIndeterminate(false);
				pDialog.setCancelable(true);
				pDialog.show();
				
			}

			/**
			 * Creating product
			 * */
			protected String doInBackground(String... args) {
				

				
				// Building Parameters
				List<NameValuePair> params = new ArrayList<NameValuePair>();
				params.add(new BasicNameValuePair("email",doctoEmail));		
				

				// getting JSON Object
				// Note that create product url accepts POST method
				jsonn = jsonParser.makeHttpRequest(doctorsInfoUrl,"POST", params);
				
				
try {
		
	username=jsonn.getString("name");
	email=jsonn.getString("email");
	doctorsName=jsonn.getString("doctorsname");
	qualification=jsonn.getString("qualification");

	designation=jsonn.getString("designation");
	institute=jsonn.getString("institute");
	address=jsonn.getString("address");
	phoneNo=jsonn.getString("phone");
	mobileNo=jsonn.getString("mobile");
	visitingTime=jsonn.getString("visitingtime");
	noOfPatient=jsonn.getString("noofpatient");
} catch (Exception e) {
	// TODO: handle exception
}
		
				return null;
			}

			/**
			 * After completing background task Dismiss the progress dialog
			 * **/
			protected void onPostExecute(String file_url) {
				// dismiss the dialog once done
				//ds=Integer.parseInt(dudhvatScore);
				doctorsNameEditText.setText(doctorsName);
				qualificationEditText.setText(qualification);
				
				userNameEditText.setText(username);
				emailEditText.setText(email);
				
				designationEditText.setText(designation);
				instituteEditText.setText(institute);
				addressEditText.setText(address);
				phoneNoEditText.setText(phoneNo);
				mobileNoEditText.setText(mobileNo);
				visitingTimeEditText.setText(visitingTime);
				noOfPatientEditText.setText(noOfPatient);
				pDialog.dismiss();
				
			}	
	
	 }

}
