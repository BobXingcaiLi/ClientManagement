using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ClientManagement.Application.Features.Client.Queries.GetClientDetail;

public record GetClientDetailQuery(int Id) : IRequest<ClientDetailDto>;
