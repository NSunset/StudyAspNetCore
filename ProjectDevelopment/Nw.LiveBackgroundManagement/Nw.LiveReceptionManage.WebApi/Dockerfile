#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#容器里的工作目录
WORKDIR /app
#拷贝发布后的文件到容器工作目录;  .是指发布文件目录位置，如果整个项目生成镜像就注释这里，使用下面注释的
COPY . /app
#容器端口
EXPOSE 80
EXPOSE 443

# 整个项目生成镜像需要。目前是手动生成发布文件来生成镜像，不需要
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["Nw.LiveReceptionManage.WebApi/Nw.LiveReceptionManage.WebApi.csproj", "Nw.LiveReceptionManage.WebApi/"]
#COPY ["Nw.LiveBackgroundManage.HangfireService/Nw.LiveBackgroundManage.HangfireService.csproj", "Nw.LiveBackgroundManage.HangfireService/"]
#COPY ["Nw.LiveBackgroundManagement.Business.Service/Nw.LiveBackgroundManagement.Business.Service.csproj", "Nw.LiveBackgroundManagement.Business.Service/"]
#COPY ["Nw.LiveBackgroundManagement.Common/Nw.LiveBackgroundManagement.Common.csproj", "Nw.LiveBackgroundManagement.Common/"]
#COPY ["RedisHelper/RedisHelper.csproj", "RedisHelper/"]
#COPY ["Nw.LiveBackgroundManagement.DataAccessEFCore/Nw.LiveBackgroundManagement.DataAccessEFCore.csproj", "Nw.LiveBackgroundManagement.DataAccessEFCore/"]
#COPY ["Nw.LiveBackgroundManagement.Models/Nw.LiveBackgroundManagement.Models.csproj", "Nw.LiveBackgroundManagement.Models/"]
#COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
#COPY ["EventBus/EventBus.csproj", "EventBus/"]
#COPY ["Nw.LiveBackgroundManagement.Business.Interface/Nw.LiveBackgroundManagement.Business.Interface.csproj", "Nw.LiveBackgroundManagement.Business.Interface/"]
#RUN dotnet restore "Nw.LiveReceptionManage.WebApi/Nw.LiveReceptionManage.WebApi.csproj"
#COPY . .
#WORKDIR "/src/Nw.LiveReceptionManage.WebApi"
#RUN dotnet build "Nw.LiveReceptionManage.WebApi.csproj" -c Release -o /app/build

#发布,目前是手动发布，不需要,如果是直接拉整个项目到服务器生成镜像，就解开上面和这里的注释，直接使用就行了。
#RUN dotnet publish "Nw.LiveReceptionManage.WebApi.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nw.LiveReceptionManage.WebApi.dll"]