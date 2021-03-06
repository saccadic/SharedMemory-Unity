//
//  Plugin.cpp
//  SharedMemoryForUnity
//
//  Created by Hilab on 2019/02/13.
//  Copyright © 2019 Hilab. All rights reserved.
//

#include "Plugin.pch"
#include "Poco/SharedMemory.h"

static Poco::SharedMemory server;

static Poco::SharedMemory client;
static Poco::SharedMemory client_img;
static int bufferSize_server;
static int bufferSize;
static int bufferSize_img;

void setupSharedMemory_server(char* name, int size){
    bufferSize_server = size;
    server = Poco::SharedMemory(name, bufferSize_server, Poco::SharedMemory::AccessMode::AM_WRITE, 0, true);
}

void setupSharedMemory(char* name, int size){
    bufferSize = size;
    client = Poco::SharedMemory(name, bufferSize, Poco::SharedMemory::AccessMode::AM_READ, 0, false);
}

void setupSharedMemory_img(char* name, int size){
    bufferSize_img = size;
    client_img = Poco::SharedMemory(name, bufferSize_img, Poco::SharedMemory::AccessMode::AM_READ, 0, false);
}

char* getMsg(){
    //memcpy(msg, client.begin(), bufferSize);
    
    return client.begin();
}

char* getImg(){
    //memcpy(msg, client.begin(), bufferSize);
    
    return client_img.begin();
}

void setMsg(char* src){
    memcpy(server.begin(), src, bufferSize_server);
}
