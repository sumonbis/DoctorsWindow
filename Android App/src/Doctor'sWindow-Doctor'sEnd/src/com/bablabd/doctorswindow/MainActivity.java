package com.bablabd.doctorswindow;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Calendar;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.net.ParseException;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.bablabd.library.UserFunctions;

@TargetApi(Build.VERSION_CODES.HONEYCOMB)
public class MainActivity extends ActionBarActivity {
	

	ArrayList<Patient> patientList;
	
	PatientAdapter adapter;
	UserFunctions userFunctions;
	private DatePicker datePicker;
	private Calendar calendar;
	private TextView dateView;
	public static String doctEmail;
	private int year, month, day;
	private String dates;
	Button refreshButton;
	String dudhvatScore="g";
	private String appUrl ="http://bablabd.com/docwin/appointment.php?doctor="+dudhvatScore+"&date=";
	
	
	@SuppressLint("NewApi")
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
	    int ds;
		try {
			FileInputStream fin = openFileInput("doctorsemal");

			int c;

			String temp = "";
			while ((c = fin.read()) != -1) {
				temp = temp + Character.toString((char) c);
			}

			dudhvatScore = temp;

		} catch (Exception ex) {
		}
		appUrl ="http://bablabd.com/docwin/appointment.php?doctor="+dudhvatScore+"&date=";
		userFunctions = new UserFunctions();
        if(userFunctions.isUserLoggedIn(getApplicationContext())){
        	setContentView(R.layout.activity_main);
        	doctEmail=LoginActivity.docEmail;
        	dateView = (TextView) findViewById(R.id.datetextview);
    		refreshButton=(Button)findViewById(R.id.refreshButton);
    		calendar = Calendar.getInstance();
    		year = calendar.get(Calendar.YEAR);
    		month = calendar.get(Calendar.MONTH);
    		day = calendar.get(Calendar.DAY_OF_MONTH);
    		showDate(year, month + 1, day);	
    		dates=dateView.getText().toString();
    		new JSONAsyncTask().execute(appUrl+dates);
    		
    		refreshButton.setOnClickListener(new OnClickListener() {
    			
    			@Override
    			public void onClick(View v) {
    				// TODO Auto-generated method stub
    				patientList.clear();
    				dates=dateView.getText().toString();
    				new JSONAsyncTask().execute(appUrl+dates);
    			}
    		});
    		
    		patientList = new ArrayList<Patient>();
    		ListView listview = (ListView)findViewById(R.id.patientlistView);
    		adapter = new PatientAdapter(getApplicationContext(), R.layout.list_row, patientList);
    		
    		listview.setAdapter(adapter);
    		
    		listview.setOnItemClickListener(new OnItemClickListener() {

    			@Override
    			public void onItemClick(AdapterView<?> arg0, View arg1, int position,
    					long id) {
    				// TODO Auto-generated method stub
    				Toast.makeText(getApplicationContext(), patientList.get(position).getPatientName(), Toast.LENGTH_LONG).show();				
    			}
    		});
    		
    		getActionBar().setTitle("Appointments");
        	
        	
        }else{
        	// user is not logged in show login screen
        	Intent login = new Intent(getApplicationContext(), LoginActivity.class);
        	login.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        	startActivity(login);
        	// Closing dashboard screen
        	finish();
        }
        
		
		
	}

	@SuppressWarnings("deprecation")
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
			dates=dateView.getText().toString();
			
			
		}
	};

	private void showDate(int year, int month, int day) {
		dateView.setText(new StringBuilder().append(day).append("/")
				.append(month).append("/").append(year));
		
		
	}

class JSONAsyncTask extends AsyncTask<String, Void, Boolean> {
		
		ProgressDialog dialog;
		
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			dialog = new ProgressDialog(MainActivity.this);
			dialog.setMessage("Loading, please wait");
			
			dialog.show();
			dialog.setCancelable(false);
		}
		
		@Override
		protected Boolean doInBackground(String... urls) {
			try {
				
				//------------------>>
				HttpGet httppost = new HttpGet(urls[0]);
				HttpClient httpclient = new DefaultHttpClient();
				HttpResponse response = httpclient.execute(httppost);

				// StatusLine stat = response.getStatusLine();
				int status = response.getStatusLine().getStatusCode();

				if (status == 200) {
					HttpEntity entity = response.getEntity();
					String data = EntityUtils.toString(entity);
					
				
					JSONObject jsono = new JSONObject(data);
					JSONArray jarray = jsono.getJSONArray("patient");
					
					for (int i = 0; i < jarray.length(); i++) {
						JSONObject object = jarray.getJSONObject(i);
					
						Patient patient = new Patient();
						
						patient.setPatientSerialNo(object.getString("serial"));
						patient.setPatientName(object.getString("name"));
						patient.setPatientAge(object.getString("age"));
						patient.setPatientSex(object.getString("sex"));
						patient.setPatientMobileNo(object.getString("mobileno"));
						
						
						patientList.add(patient);
					}
					return true;
				}
				
				//------------------>>
				
			} catch (ParseException e1) {
				e1.printStackTrace();
			} catch (IOException e) {
				e.printStackTrace();
			} catch (JSONException e) {
				e.printStackTrace();
			}
			return false;
		}
		
		protected void onPostExecute(Boolean result) {
			dialog.cancel();
			adapter.notifyDataSetChanged();
			if(result == false)
				Toast.makeText(getApplicationContext(), "Unable to fetch data from server", Toast.LENGTH_LONG).show();

		}
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		getMenuInflater().inflate(R.menu.prof, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		
		switch (item.getItemId()) {
		
		
		
		case R.id.edit:
			 Intent i=new Intent(MainActivity.this,Profile.class);
			 startActivity(i);
			return true;
		case R.id.profilepic:
			 
			Intent ip=new Intent(MainActivity.this,ProfilePicture.class);
			 startActivity(ip);
			return true;
		case R.id.logout:
			userFunctions.logoutUser(getApplicationContext());
			Intent login = new Intent(getApplicationContext(), LoginActivity.class);
        	login.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        	startActivity(login);
        	// Closing dashboard screen
        	finish();
			return true;
		default:
			return super.onOptionsItemSelected(item);
		}
	}
}
