package com.bablabd.patientswindow;

import java.io.FileInputStream;
import java.io.FileOutputStream;

import android.app.ActionBar;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.provider.Settings;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

public class Splash extends Activity  {
	
	boolean isNetworkEnabled = false;
	

	
	ImageView myView;
	TextView nameTv;
	TextView dot;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// Custom animation on image

		ActionBar acB = getActionBar();
		
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.splash);
		myView = (ImageView) findViewById(R.id.animation);
		nameTv = (TextView) findViewById(R.id.apppName);
		dot = (TextView) findViewById(R.id.dot);
		final Animation animationDot = AnimationUtils.loadAnimation(
				Splash.this, R.anim.abc_fade_in);
		final Animation animationFadeIn = AnimationUtils.loadAnimation(
				Splash.this, R.anim.abc_slide_in_top);
		final Animation aniFadeIn = AnimationUtils.loadAnimation(Splash.this,
				R.anim.abc_slide_in_bottom);
		animationDot.setDuration(7000);
		animationFadeIn.setDuration(1500);
		aniFadeIn.setDuration(1500);
		dot.setAnimation(animationDot);
		nameTv.setAnimation(aniFadeIn);
		myView.startAnimation(animationFadeIn);

		
		

		LinearLayout ll = (LinearLayout) findViewById(R.id.linlay);
		ll.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				final Animation Out = AnimationUtils.loadAnimation(Splash.this,
						R.anim.abc_fade_in);
				final Animation animationFadeOut = AnimationUtils
						.loadAnimation(Splash.this, R.anim.abc_slide_out_top);
				final Animation aniFadeOut = AnimationUtils.loadAnimation(
						Splash.this, R.anim.abc_slide_out_bottom);
				nameTv.startAnimation(aniFadeOut);
				aniFadeOut.setDuration(1000);
				Out.setDuration(7000);
				dot.setAnimation(Out);
				myView.startAnimation(animationFadeOut);
				animationFadeOut.setDuration(1000);
				// After 3 seconds redirect to another intent
				
					if (checkInternetConenction()) {

						

							Intent it = new Intent(Splash.this,SpecialityList.class);

							startActivity(it);

						
					} else {
						showNetworkSettingsAlert();

					}
				

			}
		});

		

	}

	
	

	private void showNetworkSettingsAlert() {
		AlertDialog.Builder alertDialog = new AlertDialog.Builder(Splash.this);

		alertDialog.setIcon(R.drawable.wifi);

		// Setting Dialog Title
		alertDialog.setTitle("Network Settings");

		// Setting Dialog Message
		alertDialog
				.setMessage("Internet is not enabled. Enable Internet Connection first.");

		// On pressing Settings button

		alertDialog.setPositiveButton("Settings",
				new DialogInterface.OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						startActivity(new Intent(
								android.provider.Settings.ACTION_WIFI_SETTINGS));
						dialog.cancel();
					}
				});
		// on pressing cancel button
		alertDialog.setNegativeButton("Cancel",
				new DialogInterface.OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						dialog.cancel();
					}
				});

		// Showing Alert Message
		alertDialog.show();

	}

	private boolean checkInternetConenction() {
		ConnectivityManager check = (ConnectivityManager) getApplicationContext()
				.getSystemService(Context.CONNECTIVITY_SERVICE);
		if (check != null) {
			NetworkInfo[] info = check.getAllNetworkInfo();
			if (info != null)
				for (int i = 0; i < info.length; i++)
					if (info[i].getState() == NetworkInfo.State.CONNECTED) {
						return true;
					}

		}
		return false;

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// TODO Auto-generated method stub
		getMenuInflater().inflate(R.menu.main, menu);
		// getMenuInflater().inflate(R.menu.man, menu);
		return true;

	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		// Respond to the action bar's Up/Home button

		case R.id.action_settings:
			

			return true;

		}
		return super.onOptionsItemSelected(item);
	}

}
