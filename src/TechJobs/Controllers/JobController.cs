using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
			List<Job> job = jobData.Find(id);
			return View(job);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
			// TODO #6 - Validate the ViewModel and if valid, create a 
			// new Job and add it to the JobData data store. Then
			// redirect to the Job detail (Index) action/view for the new Job
			if (ModelState.IsValid)
			{
				Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!ITS WORKING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

				Employer employer = jobData.Employers.Find(newJobViewModel.EmployerID);
				Location location = jobData.Locations.Find(newJobViewModel.Location);
				PositionType positionType = jobData.PositionTypes.Find(newJobViewModel.PositionType);
				CoreCompetency coreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetency);

				Job newJob = new Job
				{
					Name = newJobViewModel.Name,
					Employer = employer,
					Location = location,
					PositionType = positionType,
					CoreCompetency = coreCompetency
				};
				jobData.Jobs.Add(newJob);

				List<Job> job = jobData.Find(newJob.ID);
				
				return View("Index", job);
				
			}
			Debug.WriteLine("************************NOT WORKING**************************************");
			return View(newJobViewModel);
        }
    }
}
