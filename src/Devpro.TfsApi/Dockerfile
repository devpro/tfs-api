﻿FROM microsoft/dotnet:latest

RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
	&& echo "deb http://download.mono-project.com/repo/debian wheezy-libjpeg62-compat main" | tee -a /etc/apt/sources.list.d/mono-xamarin.list \
	&& echo "deb http://download.mono-project.com/repo/debian wheezy/snapshots/4.6.1 main" | tee -a /etc/apt/sources.list.d/mono-xamarin.list \
	&& apt-get update \
	&& apt-get install -y mono-devel \
	&& rm -rf /var/lib/apt/lists/*

RUN ln -s /usr/share/dotnet/shared/Microsoft.NETCore.App/1.0.1/System.Native.so /usr/lib/libSystem.Native.so

RUN ln -s /usr/share/dotnet/shared/Microsoft.NETCore.App/1.0.1/libuv.so /usr/lib/libuv.so

RUN ldconfig

ARG source=.
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
COPY $source .
