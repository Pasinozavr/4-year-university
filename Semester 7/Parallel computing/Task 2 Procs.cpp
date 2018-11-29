#include <iostream>
#include <stdio.h>
#include <mpi.h>
#include "windows.h"
#include "psapi.h"
#pragma comment (lib, "psapi.lib")

int main(int argc, char* argv [])
{
	int rank, n, i, message;
	MPI_Status status;
	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &n);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	if(rank == 0){
	printf("\n Hello from process %3d",rank);
	for (i=1;i<n;i++)
	{
	MPI_Recv(&message, 1, MPI_INT, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status);
    HANDLE hProcess = GetCurrentProcess();
    PROCESS_MEMORY_COUNTERS pmc;
    ZeroMemory(&pmc, sizeof(PROCESS_MEMORY_COUNTERS));
    GetProcessMemoryInfo(hProcess, &pmc, sizeof(PROCESS_MEMORY_COUNTERS));
	printf("\n Hello from process %3d, that use %d kb ",message,pmc.WorkingSetSize / 1024);
	}
	}
	else
	{
	MPI_Send(&rank,1,MPI_INT,0,0,MPI_COMM_WORLD);
	MPI_Finalize();
	}
	return 0;
}
	