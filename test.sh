#!/bin/bash
if [ $# -gt 0 ]; then
    count=$1
else
    count=8
fi
echo $count
cd program
for option in {LIST,LIST_INIT,YIELD,ENUM}; do
    echo option=$option
    dotnet clean > /dev/null;
    dotnet build -c:Release -o:out -p:DefineConstants=$option
    rm ../result_$option.txt 2> /dev/null
    for ((i=0;i<$count;i++)); do
        dotnet out/program.dll > /dev/null 2>> ../result_$option.txt;
    done
done
cd ../aggregate
dotnet build -c:Release -o:out
for option in {LIST,LIST_INIT,YIELD,ENUM}; do
    echo option=$option
    dotnet out/aggregate.dll $count <../result_$option.txt
done