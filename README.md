# Smaller
Make small of your repeated tasks.

## What is smaller

Smaller is a tool for managing tasks that need to happen on a regular basis - like weekly emails on regular topics as happens in academia (Lab Reports Due, Lab Prep, Tuts due, etc).

## What's Supported

Right now, only simple email, but this will likely change as I extend it for myself.

## What's in the pipeline

Basically:

1.  GUI
1.  Scheduling Integration
1.  Event Reporting
1.  CLI


### GUI

Right now the only way to add tasks is by editing the XML file. Also there are no notifications that anything happened or broke. This is literally a barebones app at the moment. Some planned features are:

1.  Run History Window with a list
1.  Task editor list
1.  Task run confirmation window - confirms that you're finished what you started
1.  Systray toggling

### Scheduling Integration

Right now, Smaller depends on you setting up a Windows scheduler to run your tasks, or for you to run them manually. I plan to provide a tool that sets up a schedule, or to use an independent scheduling.

### Event Reporting

Write run logs to your windows event log.

### CLI

Maintain tasks, scheduling and querying.

## Setting up smaller

It's a little technical, but shouldn't be too hard for any programmer:

1.  Unpack the dsitributable into your folder of choice.
2.  Create a windows scheduler item that runs `Path\To\Smaller.exe` on a schedule that works for you, say every day at 8am.
3.  Edit the `.big` file to execute your tasks

Voile!

## Running Smaller

When you run smaller, it lives as a systray icon. Smaller runs jobs on startup, or you can manually ask it to run by right-clicking on the tray  icon and selecting `Run Now`. 

## Big Files
Big files contain the tasks and their due dates which you would like performed. They are formatted XML files which contain tasks and parameters that can be updated.

A Minimal Big file looks like this:

```
<?xml version="1.0"?>
<SmallerTaskList xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Tasks>
    <SmallerTaskBase xsi:type="MailTask">
      <ScheduledDate>2016-02-10T00:00:00</ScheduledDate>
      <Identifier>060942d3-0627-4804-8fe5-d69f36c6c397</Identifier>
      <To>aaa@bbb.com</To>
      <Subject>Bar</Subject>
      <Body>Foo
Bar</Body>
    </SmallerTaskBase>
  </Tasks>
  <Parameters>
    <Parameter>
      <Key>ADMINISTRATOR</Key>
      <Value>John</Value>
    </Parameter>
  </Parameters>
</SmallerTaskList>
```

You can create a sample file by selecting "Sample Out" from your system tray.

## License

Currently licensed as AGPL V3.0