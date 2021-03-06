/// \file DS_ThreadsafeAllocatingQueue.h
/// \internal
/// A threadsafe queue, that also uses a memory pool for allocation

#ifndef __THREADSAFE_ALLOCATING_QUEUE
#define __THREADSAFE_ALLOCATING_QUEUE

#include "DS_Queue.h"
#include "SimpleMutex.h"
#include "DS_MemoryPool.h"

#if defined(new)
#pragma push_macro("new")
#undef new
#define RMO_NEW_UNDEF_ALLOCATING_QUEUE
#endif

namespace DataStructures
{

template <class structureType>
class RAK_DLL_EXPORT ThreadsafeAllocatingQueue
{
public:
	// Queue operations
	void Push(structureType *s);
	structureType *PopInaccurate(void);
	structureType *Pop(void);
	void SetPageSize(int size);

	// Memory pool operations
	structureType *Allocate(const char *file, unsigned int line);
	void Deallocate(structureType *s, const char *file, unsigned int line);
	void Clear(const char *file, unsigned int line);
protected:

	MemoryPool<structureType> memoryPool;
	SimpleMutex memoryPoolMutex;
	Queue<structureType*> queue;
	SimpleMutex queueMutex;
};
	
template <class structureType>
void ThreadsafeAllocatingQueue<structureType>::Push(structureType *s)
{
	queueMutex.Lock();
	queue.Push(s, __FILE__, __LINE__ );
	queueMutex.Unlock();
}

template <class structureType>
structureType *ThreadsafeAllocatingQueue<structureType>::PopInaccurate(void)
{
	structureType *s;
	if (queue.IsEmpty())
		return 0;
	queueMutex.Lock();
	if (queue.IsEmpty()==false)
		s=queue.Pop();
	else
		s=0;
	queueMutex.Unlock();	
	return s;
}

template <class structureType>
structureType *ThreadsafeAllocatingQueue<structureType>::Pop(void)
{
	structureType *s;
	queueMutex.Lock();
	if (queue.IsEmpty())
	{
		queueMutex.Unlock();	
		return 0;
	}
	s=queue.Pop();
	queueMutex.Unlock();	
	return s;
}

template <class structureType>
structureType *ThreadsafeAllocatingQueue<structureType>::Allocate(const char *file, unsigned int line)
{
	structureType *s;
	memoryPoolMutex.Lock();
	s=memoryPool.Allocate(file, line);
	memoryPoolMutex.Unlock();
	// Call new operator, memoryPool doesn't do this
	s = new ((void*)s) structureType;
	return s;
}
template <class structureType>
void ThreadsafeAllocatingQueue<structureType>::Deallocate(structureType *s, const char *file, unsigned int line)
{
	// Call delete operator, memory pool doesn't do this
	s->~structureType();
	memoryPoolMutex.Lock();
	memoryPool.Release(s, file, line);
	memoryPoolMutex.Unlock();
}

template <class structureType>
void ThreadsafeAllocatingQueue<structureType>::Clear(const char *file, unsigned int line)
{
	memoryPoolMutex.Lock();
	for (unsigned int i=0; i < queue.Size(); i++)
	{
		queue[i]->~structureType();
		memoryPool.Release(queue[i], file, line);
	}
	queue.Clear(file, line);
	memoryPoolMutex.Unlock();
	memoryPoolMutex.Lock();
	memoryPool.Clear(file, line);
	memoryPoolMutex.Unlock();
}

template <class structureType>
void ThreadsafeAllocatingQueue<structureType>::SetPageSize(int size)
{
	memoryPool.SetPageSize(size);
}

};


#if defined(RMO_NEW_UNDEF_ALLOCATING_QUEUE)
#pragma pop_macro("new")
#undef RMO_NEW_UNDEF_ALLOCATING_QUEUE
#endif

#endif
