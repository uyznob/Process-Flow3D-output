clc; clear all;
ntl = 5;                            %Number of  NaN in those first line
nnode = 408;                %Number of nodes
ntstep = 3505;              %Number of timestep
nof = 2;                          %Number of pressure worksheet

%% Input data
%!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
%Please select version of flow3d model and other inputs

p = [];
for i = 1:nof    
    p = [p; xlsread('Extract Pressure',['P' num2str(i)])];
end

%% Divide data along with timestep
%These following codes are based on
%fixed format of transf.out from Flow3d

pt = [];                 %pressure along with timestep
for j = 1:ntstep
    pt(1:nnode,1:4,j) = p((ntl+1)*j+nnode*(j-1):(ntl+1)*j+nnode*j-1,1:4);
end

%pt data is arranges as 3D matrix
%First 2 dimensions are node and pressure
%Last dimension is timestep