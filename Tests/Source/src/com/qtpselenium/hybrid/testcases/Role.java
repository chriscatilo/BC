package com.qtpselenium.hybrid.testcases;

import java.lang.reflect.InvocationTargetException;
import java.util.Hashtable;

import org.testng.SkipException;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.AfterTest;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import com.qtpselenium.hybrid.util.Keywords;
import com.qtpselenium.hybrid.util.TestUtil;

public class Role {
	
	

	@Test(dataProvider="getshareData")
	public void BuildingManagementTest(Hashtable<String,String> data) throws NumberFormatException, InterruptedException{
		
		if(!TestUtil.isTestCaseExecutable("Role", Keywords.xls))
		  throw new SkipException("Skipping the test as Runmode is NO");
		if(!data.get("RunMode").equals("Y"))
			  throw new SkipException("Skipping the test as data set Runmode is NO");

		System.out.println("***Role Test Case  is started***");
		Keywords k = Keywords.getKeywordsInstance();
		k.executeKeywords("Role",data);
		
		
	}
		
	
		@DataProvider
		public Object[][] getshareData(){
			return TestUtil.getData("Role", Keywords.xls);
		}

}
