package com.bablabd.doctorswindow;

public class Patient {
	
	private String patientSerialNo;
	private String patientName;	
	private String patientAge;
	private String patientSex;
	private String patientMobileNo;
	

	public Patient() {
		// TODO Auto-generated constructor stub
	}


	public Patient(String patientSerialNo, String patientName,
			String patientAge, String patientSex, String patientMobileNo) {
		super();
		this.patientSerialNo = patientSerialNo;
		this.patientName = patientName;
		this.patientAge = patientAge;
		this.patientSex = patientSex;
		this.patientMobileNo = patientMobileNo;
	}


	public String getPatientSerialNo() {
		return patientSerialNo;
	}


	public void setPatientSerialNo(String patientSerialNo) {
		this.patientSerialNo = patientSerialNo;
	}


	public String getPatientName() {
		return patientName;
	}


	public void setPatientName(String patientName) {
		this.patientName = patientName;
	}


	public String getPatientAge() {
		return patientAge;
	}


	public void setPatientAge(String patientAge) {
		this.patientAge = patientAge;
	}


	public String getPatientSex() {
		return patientSex;
	}


	public void setPatientSex(String patientSex) {
		this.patientSex = patientSex;
	}


	public String getPatientMobileNo() {
		return patientMobileNo;
	}


	public void setPatientMobileNo(String patientMobileNo) {
		this.patientMobileNo = patientMobileNo;
	}

	


	

}
