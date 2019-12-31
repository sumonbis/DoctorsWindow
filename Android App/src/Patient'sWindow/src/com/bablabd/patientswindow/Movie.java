package com.bablabd.patientswindow;

import java.util.ArrayList;

public class Movie {
	private String title, thumbnailUrl,qualification,mobileno,address,email;


	public String getEmail() {
		return email;
	}



	public void setEmail(String email) {
		this.email = email;
	}



	public String getTitle() {
		return title;
	}



	public void setTitle(String title) {
		this.title = title;
	}



	public String getThumbnailUrl() {
		return thumbnailUrl;
	}



	public void setThumbnailUrl(String thumbnailUrl) {
		this.thumbnailUrl = thumbnailUrl;
	}



	public String getQualification() {
		return qualification;
	}



	public void setQualification(String qualification) {
		this.qualification = qualification;
	}



	public String getMobileno() {
		return mobileno;
	}



	public void setMobileno(String mobileno) {
		this.mobileno = mobileno;
	}



	public String getAddress() {
		return address;
	}



	public void setAddress(String address) {
		this.address = address;
	}



	public Movie() {
	}

	

	public Movie(String title, String thumbnailUrl, String qualification,
			String mobileno, String address, String email) {
		super();
		this.title = title;
		this.thumbnailUrl = thumbnailUrl;
		this.qualification = qualification;
		this.mobileno = mobileno;
		this.address = address;
		this.email = email;
	}



	



	

}
