*******************************************************************************************
PROJECT DETAILS
*******************************************************************************************

1. Azure Mobile Service
-------------------------------------------------------------------------------------------
•	To run the application using the deployed Azure Mobile Service, please use the following steps:
	1) Find and open the unity container factory: \Rightpoint.Peeps.Client\DI\UnityContainerFactory.cs.
	2) Find the injection factory for the PeepsMobileServiceClient.
	3) Note it takes in the following details: ("applicationUrl", "applicationKey").
	4) Please reach out to project admins for these details and replace them appropriately:
		https://github.com/RightpointLabs/Peeps
	5) DO NOT check these settings into source control.
	   TODO - we need to read these from a local config outside of source control.

2. Azure Application Insights
-------------------------------------------------------------------------------------------
•	To run the application connected to Azure Application Insights, please use the following steps:
	1) Find and open the ApplicationInsights.config in the Rightpoint.Peeps.Client project.
	2) Find the InstrumentationKey element.
	3) Please reach out to project admins for this key and replace it appropriately:
		https://github.com/RightpointLabs/Peeps
	4) DO NOT check these settings into source control.
	   TODO - we need to read this from a local config outside of source control.
	5) Go to the App.xaml code behind and uncomment the InitializeAsync for Application Insights.
