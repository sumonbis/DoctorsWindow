package com.bablabd.patientswindow;

import android.app.ListActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class SpecialityList extends ListActivity {
	public static String sp; 
		static final String[] Categories = new String[] { "Medicine","Audiologists",
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
	 
		@Override
		public void onCreate(Bundle savedInstanceState) {
			super.onCreate(savedInstanceState);
	 
			// no more this
			// setContentView(R.layout.list_fruit);
	 
			setListAdapter(new ArrayAdapter<String>(this, R.layout.cat_list,Categories));
	 
			ListView listView = getListView();
			listView.setTextFilterEnabled(true);
	 
			listView.setOnItemClickListener(new OnItemClickListener() {
				public void onItemClick(AdapterView<?> parent, View view,
						int position, long id) {
				    // When clicked, show a toast with the TextView text
					sp=(String) ((TextView) view).getText();
					
				    
				    Intent in=new Intent(SpecialityList.this,MainActivity.class);
				    startActivity(in);
				    
				}
			});
	 
		}
	 
	}
