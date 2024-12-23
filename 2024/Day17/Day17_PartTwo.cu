#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <chrono>

#include <iostream>

#define NUMBER_OF_BLOCKS 256
#define NUMBER_OF_THREADS 1024

#define CHECKS_PER_THREAD 16348


__global__ void partTwo(unsigned long long int startA, unsigned long long int *result) {
    unsigned long long int idx = blockIdx.x * blockDim.x + threadIdx.x;

    unsigned long long int start = startA + idx * CHECKS_PER_THREAD;

    constexpr uint64_t target = 2417754114550330ULL;

    for (unsigned long long int i = start; i < start + CHECKS_PER_THREAD; i++) {
        uint64_t calculatedOutput = 0;

        uint64_t a = i;
        uint64_t b;
        uint64_t c;

        while (a != 0) {
            b = a & 0b111;
            b = b ^ 7;
            c = a >> b;
            b = b ^ c;
            b = b ^ 4;
            calculatedOutput = calculatedOutput * 10 + (b & 0b111);
            a >>= 3;
        }

        if (calculatedOutput == target) {
            atomicMin(result, i);
        }
    }
}

int main() {
    using namespace std::chrono;
    auto startTime = duration_cast<milliseconds>(system_clock::now().time_since_epoch());

    uint64_t start = 0;

    while (true) {

        std::cout << start << std::endl;

        uint64_t *result;
        cudaMallocManaged(&result, sizeof(uint64_t));
        *result = ULLONG_MAX;

        partTwo<<<256, 1024>>>(start, result);
        cudaDeviceSynchronize();

        if (*result != ULLONG_MAX) {
            std::cout << "Result: " << *result << std::endl;

            auto endTime = duration_cast<milliseconds>(system_clock::now().time_since_epoch());
            std::cout << "Time needed in ms: " << endTime - startTime << std::endl;

            return 1;
        }

        cudaFree(result);

        start += (unsigned long long int)(NUMBER_OF_BLOCKS) * (unsigned long long int)NUMBER_OF_THREADS * (unsigned long long int)CHECKS_PER_THREAD;
    }
}