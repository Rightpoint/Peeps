*******************************************************************************************
PROJECT DETAILS
*******************************************************************************************

1. Azure Mobile Service
-------------------------------------------------------------------------------------------
•	To run the application using the deployed Azure Mobile Service, please use the following steps:
	1) Find the unity container factory: \Rightpoint.Peeps.Client\DI\UnityContainerFactory.cs.
	2) Find the injection factory for the PeepsMobileServiceClient.
	3) Note it takes in the following details: ("applicationUrl", "applicationKey").
	4) Please reach out to project admins for these details and replace them appropriately:
		https://github.com/RightpointLabs/Peeps
	5) DO NOT check these settings into source control.
	6) TODO - we need to read these from a local config outside of source control.
