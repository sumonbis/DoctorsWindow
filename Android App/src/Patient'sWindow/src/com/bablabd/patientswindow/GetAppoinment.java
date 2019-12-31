package com.bablabd.patientswindow;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONObject;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

public class GetAppoinment extends Activity {

	ProgressDialog pDialog;
	EditText patientNameET;
	EditText patientAgeET;
	EditText patientMobileET;
	Spinner patientSexSpinner;
	Button getAppointmentButton;
	private DatePicker datePicker;
	private Calendar calendar;
	private TextView dateView;
	private int year, month, day;

	String[] sexs = { "Female", "Male" };
	String name;
	String sex;
	String age;
	String mobileno;
	String date;
	String outPut;

	JSONObject json;
	PHPRequest jsonParser = new PHPRequest();

	// url to create new product
	private static String getAppointmentUrl = "http://bablabd.com/docwin/getappointment.php";

	// JSON Node names
	public static final String TAG_SUCCESS = "success";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.getappointment);
		dateView = (TextView) findViewById(R.id.datetextview);
		calendar = Calendar.getInstance();
		year = calendar.get(Calendar.YEAR);
		month = calendar.get(Calendar.MONTH);
		day = calendar.get(Calendar.DAY_OF_MONTH);
		showDate(year, month + 1, day);
		getActionBar().setTitle(MainActivity.nameSingle);
		patientNameET = (EditText) findViewById(R.id.patientNameEditText);
		patientAgeET = (EditText) findViewById(R.id.patientAgeEditText);
		patientMobileET = (EditText) findViewById(R.id.patientMobileEditText);
		patientSexSpinner = (Spinner) findViewById(R.id.patientSexSpinner);
		getAppointmentButton = (Button) findViewById(R.id.appbtn);

		getAppointmentButton.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				name = patientNameET.getText().toString();
				age = patientAgeET.getText().toString();
				mobileno = patientMobileET.getText().toString();
				date = dateView.getText().toString();
				if(name.equals("")||age.equals("")||mobileno.equals(""))
				{
					Toast.makeText(getApplicationContext(), "Please fill all the fields.",Toast.LENGTH_SHORT).show();
				}
				else
				{
				new appointmentAsync().execute();
				}

			}
		});

		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
				android.R.layout.simple_spinner_dropdown_item, sexs);
		patientSexSpinner.setAdapter(adapter);
		patientSexSpinner
				.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
					int count = 0;

					@Override
					public void onItemSelected(AdapterView<?> arg0, View arg1,
							int arg2, long arg3) {

						int position = patientSexSpinner
								.getSelectedItemPosition();
						switch (position) {
						case 0:
							sex = "Female";
							break;
						case 1:
							sex = "Male";
							break;

						}

					}

					@Override
					public void onNothingSelected(AdapterView<?> arg0) {
						// TODO Auto-generated method stub
					}

				});

	}

	public void setDate(View view) {
		showDialog(999);

	}

	@Override
	protected Dialog onCreateDialog(int id) {
		// TODO Auto-generated method stub
		if (id == 999) {
			return new DatePickerDialog(this, myDateListener, year, month, day);
		}
		return null;
	}

	private DatePickerDialog.OnDateSetListener myDateListener = new DatePickerDialog.OnDateSetListener() {

		@Override
		public void onDateSet(DatePicker arg0, int arg1, int arg2, int arg3) {
			// TODO Auto-generated method stub
			// arg1 = year
			// arg2 = month
			// arg3 = day

			showDate(arg1, arg2 + 1, arg3);
		}
	};

	private void showDate(int year, int month, int day) {
		dateView.setText(new StringBuilder().append(day).append("/")
				.append(month).append("/").append(year));
	}

	public class appointmentAsync extends AsyncTask<String, String, String> {

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
			pDialog = new ProgressDialog(GetAppoinment.this);
			pDialog.setMessage("Loading...");
			pDialog.setIndeterminate(false);
			pDialog.setCancelable(false);
			pDialog.show();

		}

		/**
		 * Creating product
		 * */
		protected String doInBackground(String... args) {

			// Building Parameters
			List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("email", MainActivity.emal));
			params.add(new BasicNameValuePair("name", name));
			params.add(new BasicNameValuePair("age", age));
			params.add(new BasicNameValuePair("sex", sex));
			params.add(new BasicNameValuePair("mobileno", mobileno));
			params.add(new BasicNameValuePair("date", date));

			// getting JSON Object
			// Note that create product url accepts POST method
			json = jsonParser
					.makeHttpRequest(getAppointmentUrl, "POST", params);

			try {

				outPut = json.getString("ok");

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
			// ds=Integer.parseInt(dudhvatScore);

			pDialog.dismiss();
			if (outPut.equals("500")) {
				new AlertDialog.Builder(GetAppoinment.this)
						.setTitle("Not Available")
						.setMessage(
								"No time slot available on your desired date. Please try again with another date.")
						.setNegativeButton("Got it!",
								new DialogInterface.OnClickListener() {
									public void onClick(DialogInterface dialog,
											int which) {
										// do nothing
										dialog.cancel();
									}
								}).show();
			} else {
				new AlertDialog.Builder(GetAppoinment.this)
						.setTitle("Confirmation!")
						.setMessage(
								"Congratulation " + name
										+ " ! Your Appointment with "
										+ MainActivity.nameSingle
										+ " has been confirmed.\n\nDate: "
										+ date + "\nTime: "
										+ MainActivity.visitingtimeSingle
										+ "\nSerial No: " + outPut
										+ "\nAddress: "
										+ MainActivity.addressSingle)
						.setNegativeButton("Got it!",
								new DialogInterface.OnClickListener() {
									public void onClick(DialogInterface dialog,
											int which) {
										// do nothing
										dialog.cancel();
									}
								}).show();
			}

		}
	}
}