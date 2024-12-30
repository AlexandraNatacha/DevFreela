﻿using Azure;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Drawing;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQuery: IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
    }
}
