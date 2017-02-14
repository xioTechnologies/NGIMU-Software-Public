clc;
clear;
close all;

%% Import data

sessionData = importSession('Synchronisation Test', 'FileNames', {'serialCts'}); % only import serialCts.csv

%% Calculate deviation of slaves from master

masterName = 'ngimuMaster0029d557';
masterTimestamps = sessionData.(masterName).serialcts.time;

numberOfSlaves = sessionData.numberOfDevices - 1;
slaveErrors = nan(size(masterTimestamps, 1), numberOfSlaves);

slaveNames = sessionData.deviceNames(~strcmp(sessionData.deviceNames, masterName));

for timestampIndex = 1:length(masterTimestamps)
    for slaveIndex = 1:numberOfSlaves

        difference = sessionData.(slaveNames{slaveIndex}).serialcts.time - masterTimestamps(timestampIndex);

        slaveError = difference(find(abs(difference) < 0.4));

        if ~isempty(slaveError)
            slaveErrors(timestampIndex, slaveIndex) = slaveError;
        end
    end
end

%% Calculate worst-case difference between any two slaves

worstCaseDifference = max(slaveErrors')' - min(slaveErrors')';
worstCaseDifference(any(isnan(slaveErrors)')') = NaN; % omit calculations if not all device errors available

%% Plot results

figure;

time = (masterTimestamps - masterTimestamps(1)) / 60;

ax(1) = subplot(2,1,1);
hold on;
slaveColours = hsv(numberOfSlaves);
for i = 1:numberOfSlaves
    plot(time, slaveErrors(:,i) * 1000, 'Color', slaveColours(i,:));
end
set(gca,'YGrid', 'on');
xlabel('Master time (minutes)');
ylabel('Slave error relative to master (ms)');
clickableLegend(slaveNames);

ax(2) = subplot(2,1,2);
hold on;
indexSelect = ~isnan(worstCaseDifference);
plot(time(indexSelect), worstCaseDifference(indexSelect) * 1000);
set(gca,'YGrid', 'on');
xlabel('Master time (minutes)');
ylabel('Worst-case difference between any two slaves (ms)');

linkaxes(ax, 'x');
