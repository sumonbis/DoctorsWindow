package com.bablabd.patientswindow;





import java.util.ArrayList;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Toast;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonArrayRequest;

public class MainActivity extends Activity {
	// Log tag
	private static final String TAG = MainActivity.class.getSimpleName();

	// Movies json url
	public static  String url = "http://bablabd.com/docwin/doctorslist.php?speciality=";

	private ProgressDialog pDialog;
	private List<Movie> movieList = new ArrayList<Movie>();
	private ListView listView;
	private CustomListAdapter adapter;
	
	private String[] email;
	private String[] name;
	private String[] qualification;
	private String[] speciality;
	private String[] designation;
	private String[] institute;
	private String[] address;
	private String[] phone;
	private String[] mobile;
	private String[] visitingtime;
	
	public static String emal;
	public static String nameSingle;
	public static String qualificationSingle;
	public static String specialitySingle;
	public static String designationSingle;
	public static String instituteSingle;
	public static String addressSingle;
	public static String phoneSingle;
	public static String mobileSingle;
	public static String visitingtimeSingle;
	
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		
		listView = (ListView) findViewById(R.id.list);
		adapter = new CustomListAdapter(this, movieList);
		listView.setAdapter(adapter);
		getActionBar().setTitle(SpecialityList.sp);
		listView.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
			    emal=email[position];
			    nameSingle=name[position];
				qualificationSingle=qualification[position];
				specialitySingle=speciality[position];
				designationSingle=designation[position];
				instituteSingle=institute[position];
				addressSingle=address[position];
				phoneSingle=phone[position];
				mobileSingle=mobile[position];
				visitingtimeSingle=visitingtime[position];
			    
				//Toast.makeText(getApplicationContext(),email[position],Toast.LENGTH_LONG).show();
				
			    
			    Intent ink=new Intent(MainActivity.this,SingleDoctor.class);
			    startActivity(ink);
			    
			}
		});
		
		
		pDialog = new ProgressDialog(this);
		pDialog.setCancelable(false);
		// Showing progress dialog before making http request
		pDialog.setMessage("Loading...");
		pDialog.show();

		// changing action bar color
		getActionBar().setBackgroundDrawable(
				new ColorDrawable(Color.parseColor("#1b1b1b")));

		// Creating volley request obj
		JsonArrayRequest movieReq = new JsonArrayRequest(url+SpecialityList.sp,
				new Response.Listener<JSONArray>() {
					@Override
					public void onResponse(JSONArray response) {
						Log.d(TAG, response.toString());
						hidePDialog();

						email=new String[response.length()];
						name=new String[response.length()];
						qualification=new String[response.length()];
						speciality=new String[response.length()];
						designation=new String[response.length()];
						institute=new String[response.length()];
						address=new String[response.length()];
						phone=new String[response.length()];
						mobile=new String[response.length()];
						visitingtime=new String[response.length()];
						// Parsing json
						for (int i = 0; i < response.length(); i++) {
							try {

								JSONObject obj = response.getJSONObject(i);
								Movie movie = new Movie();
								movie.setTitle(obj.getString("title"));
								movie.setThumbnailUrl(obj.getString("image"));
								movie.setQualification(obj.getString("qualification"));
										
								movie.setAddress(obj.getString("address"));

								
								movie.setMobileno(obj.getString("mobileno"));
								
								email[i]=obj.getString("email");
								name[i]=obj.getString("title");
								qualification[i]=obj.getString("qualification");
								speciality[i]=obj.getString("speciality");
								designation[i]=obj.getString("designation");
								institute[i]=obj.getString("institute");
								address[i]=obj.getString("address");
								phone[i]=obj.getString("phone");
								mobile[i]=obj.getString("mobileno");
								visitingtime[i]=obj.getString("visitingtime");

								// adding movie to movies array
								movieList.add(movie);

							} catch (JSONException e) {
								e.printStackTrace();
							}

						}

						// notifying list adapter about data changes
						// so that it renders the list view with updated data
						adapter.notifyDataSetChanged();
					}
				}, new Response.ErrorListener() {
					@Override
					public void onErrorResponse(VolleyError error) {
						VolleyLog.d(TAG, "Error: " + error.getMessage());
						hidePDialog();

					}
				});

		// Adding request to request queue
		AppController.getInstance().addToRequestQueue(movieReq);
	}

	@Override
	public void onDestroy() {
		super.onDestroy();
		hidePDialog();
	}

	private void hidePDialog() {
		if (pDialog != null) {
			pDialog.dismiss();
			pDialog = null;
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

}
