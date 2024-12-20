﻿using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class AdditionalInformationRepository: Repository<AdditionalInformation>, IAdditionalInformationRepository
{
    public AdditionalInformationRepository(DocumentManagementContext context) : base(context)
    {
    }
}