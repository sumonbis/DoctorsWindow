package com.bablabd.doctorswindow;

import java.io.InputStream;
import java.util.ArrayList;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;



public class PatientAdapter extends ArrayAdapter<Patient> {
	ArrayList<Patient> patientList;
	LayoutInflater vi;
	int Resource;
	ViewHolder holder;

	
 
	
	public PatientAdapter(Context context, int resource, ArrayList<Patient> objects) {
		super(context, resource, objects);
		vi = (LayoutInflater) context
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		Resource = resource;
		patientList = objects;
	}


	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// convert view = design
		View v = convertView;
		if (v == null) {
			holder = new ViewHolder();
			v = vi.inflate(Resource, null);
			
			holder.patName= (TextView) v.findViewById(R.id.patientNameTV);
			holder.patSerial= (TextView) v.findViewById(R.id.patientSerialTV);
			holder.patAge= (TextView) v.findViewById(R.id.patientAgeTV);
			holder.patSex= (TextView) v.findViewById(R.id.patientSexTV);
			holder.patMobieNo= (TextView) v.findViewById(R.id.patientMobileNoTV);
		
			
			v.setTag(holder);
		} else {
			holder = (ViewHolder) v.getTag();
		}
		
		
		holder.patName.setText(patientList.get(position).getPatientName());
		holder.patSerial.setText(patientList.get(position).getPatientSerialNo()+".");
		holder.patAge.setText("Age: " + patientList.get(position).getPatientAge());
		holder.patSex.setText("Sex:"+patientList.get(position).getPatientSex());
		holder.patMobieNo.setText("Mobile No: " + patientList.get(position).getPatientMobileNo());
		
		return v;

	}

	static class ViewHolder {
		
		public TextView patName;
		public TextView patSerial;
		public TextView patAge;
		public TextView patSex;
		public TextView patMobieNo;
	

	}

	

		

	

}
