﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PJCAdmin.Models;

namespace PJCAdmin.Classes.Helpers.MVCModelHelpers
{
    /* --------------------------------------------------------
     * The JobHelper class provides common methods relating 
     * to Jobs for the MVC service.
     * --------------------------------------------------------
     */
    public class JobHelper
    {
        private RoutineHelper routineHelper;
        private DbHelper helper;

        public JobHelper(DbHelper h = null)
        {
            if (h == null)
                helper = new DbHelper();
            else
                helper = h;

            routineHelper = new RoutineHelper(helper);
        }

        public DbHelper getDBHelper()
        {
            return helper;
        }

        /* TODO */
        public List<Job> getAllJobsForRoutine(string creatorUsername, string assigneeUsername, string routineTitle)
        {
            List<Routine> routineList = routineHelper.getRoutinesAssignedToByName(creatorUsername, routineTitle, assigneeUsername);
            List<Job> jobList = new List<Job>();

            foreach (Routine r in routineList)
            {
                if (r.Jobs.Count() > 0)
                    jobList.AddRange(r.Jobs);
            }
            return jobList;
        }
        /* TODO */
        public List<Job> getAllJobsForRoutineVersion(string creatorUsername, string assigneeUsername, string routineTitle, DateTime updatedDate)
        {
            if (!routineHelper.routineVersionExists(creatorUsername, assigneeUsername, routineTitle, updatedDate))
                return null;

            List<Routine> assignedRoutines = routineHelper.getRoutinesAssignedToByName(creatorUsername, routineTitle, assigneeUsername);
            Routine routineVersion = assignedRoutines.Where(r => dateEquals(r.updatedDate, updatedDate)).First();

            return routineVersion.Jobs.ToList();
        }
        /* TODO */
        public Job getJob(string creatorUsername, string assigneeUsername, string routineTitle, DateTime startDate)
        {
            return getAllJobsForRoutine(creatorUsername, assigneeUsername, routineTitle).Where(j => j.startTime.Equals(startDate)).First();
        }
        /* TODO */
        public bool jobExists(string creatorUsername, string assigneeUsername, string routineTitle, DateTime startDate)
        {
            List<Job> list = getAllJobsForRoutine(creatorUsername, assigneeUsername, routineTitle);
            if (list.Count() == 0)
                return false;

            return list.Where(j => j.startTime.Equals(startDate)).Count() > 0;
        }

        /* TODO */
        private bool dateEquals(DateTime dt1, DateTime dt2)
        {
            dt1 = dt1.AddMilliseconds(-dt1.Millisecond);
            dt2 = dt2.AddMilliseconds(-dt2.Millisecond);

            return DateTime.Equals(dt1, dt2);
        }

        public void dispose()
        {
            helper.dispose();
        }
    }
}