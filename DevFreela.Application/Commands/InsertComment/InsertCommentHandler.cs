﻿using Azure.Core;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public InsertCommentHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);
            if (project is null)
                return ResultViewModel.Erro("Projeto não existe.");

            var comments = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _context.ProjectComments.AddAsync(comments);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
