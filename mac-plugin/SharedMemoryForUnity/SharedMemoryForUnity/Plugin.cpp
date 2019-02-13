//
//  Plugin.cpp
//  SharedMemoryForUnity
//
//  Created by Hilab on 2019/02/13.
//  Copyright © 2019 Hilab. All rights reserved.
//

#include "Plugin.pch"
#include "Poco/SharedMemory.h"

static Poco::SharedMemory client;
static int bufferSize;

void setupSharedMemory(char* name, int size){
    bufferSize = size;
    client = Poco::SharedMemory(name, bufferSize, Poco::SharedMemory::AccessMode::AM_READ, 0, false);
}

char* getMsg(){
    //memcpy(msg, client.begin(), bufferSize);
    
    return client.begin();
}
