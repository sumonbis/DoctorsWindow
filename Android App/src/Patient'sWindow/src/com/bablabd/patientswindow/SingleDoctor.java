package com.bablabd.patientswindow;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

public class SingleDoctor extends Activity{
	TextView nameTextView;
	TextView qualificationTextView;
	TextView specialityTextView;
	TextView designationTextView;
	TextView instituteTextView;
	TextView addressTextView;
	TextView phoneTextView;
	TextView mobileTextView;
	TextView visitingTimeTextView;
	Button getAppointmentButton;
	Button callButton;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.single_doctor);
		
		getActionBar().setTitle(MainActivity.nameSingle);
		nameTextView=(TextView)findViewById(R.id.doctorsNameTextView);
		qualificationTextView=(TextView)findViewById(R.id.qualificationTextView);
		specialityTextView=(TextView)findViewById(R.id.specialityTextView);
		designationTextView=(TextView)findViewById(R.id.designationTextView);
		instituteTextView=(TextView)findViewById(R.id.instituteTextView);
		addressTextView=(TextView)findViewById(R.id.addressTextView);
		phoneTextView=(TextView)findViewById(R.id.phoneTextView);
		mobileTextView=(TextView)findViewById(R.id.mobileTextView);
		visitingTimeTextView=(TextView)findViewById(R.id.visitingtimeTextView);
		getAppointmentButton=(Button)findViewById(R.id.getappointmentButton);
		callButton=(Button)findViewById(R.id.callButton);
		
		nameTextView.setText(MainActivity.nameSingle);
		qualificationTextView.setText(MainActivity.qualificationSingle);
		specialityTextView.setText(MainActivity.specialitySingle);
		designationTextView.setText(MainActivity.designationSingle);
		instituteTextView.setText(MainActivity.instituteSingle);
		addressTextView.setText(MainActivity.addressSingle);
		phoneTextView.setText("Phone No: "+MainActivity.phoneSingle);
		mobileTextView.setText("Mobile No: "+MainActivity.mobileSingle);
		visitingTimeTextView.setText("Visiting Time: "+MainActivity.visitingtimeSingle);
		
		
		getAppointmentButton.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent ni=new Intent(SingleDoctor.this,GetAppoinment.class);
				startActivity(ni);
				
			}
		});
		
		callButton.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent intent5 = new Intent(Intent.ACTION_CALL, Uri.parse("tel:" +MainActivity.mobileSingle));
	   			startActivity(intent5);
			}
		});
		
	}

}
