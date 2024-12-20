﻿using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class AdditionalInformationDetailRepository: Repository<AdditionalInformationDetail>, IAdditionalInformationDetailRepository
{
    public AdditionalInformationDetailRepository(DocumentManagementContext context) : base(context)
    {
    }
}