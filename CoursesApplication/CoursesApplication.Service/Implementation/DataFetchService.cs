﻿using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Domain.DTO;
using CoursesApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApplication.Service.Implementation
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly ICourseService courseService;

        public DataFetchService(ICourseService courseService, HttpClient httpClient)
        {
            this.courseService = courseService;
            _httpClient = httpClient;
        }

        public async Task<List<Course>> FetchCoursesFromApi()
        {
            string URL = "http://is-lab4.ddns.net:8080/courses";
            var data = await _httpClient.GetFromJsonAsync<List<CourseDetailsDTO>>(URL);
            var courses = data.Select(x => new Course
            {
                Id = Guid.NewGuid(),
                Name = x.Title,
                Credits = x.ECTS,
                SemesterType = x.SemesterType
            }).ToList();
            courseService.InsertMany(courses);
            return courses;
        }
    }
}
